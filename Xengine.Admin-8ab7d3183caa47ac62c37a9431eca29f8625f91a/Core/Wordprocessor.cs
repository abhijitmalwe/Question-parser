using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using OpenXmlPowerTools;
using Xengine.Admin.Models;

namespace Xengine.Admin.Core
{
    public interface IWordprocessor
    {
        string Process(string filePath);
    }

    public class Wordprocessor : IWordprocessor
    {
        private readonly ILogger<Wordprocessor> _logger;
        private readonly RegexSettings _regexSettings;

        public Wordprocessor(ILogger<Wordprocessor> log, RegexSettings regexSettings)
        {
            _logger = log;
            _regexSettings = regexSettings;
        }

        public string Process(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.LogDebug($"File {filePath} does not exists. Exiting Wordprocessor Class.");
                return null;
            }

            byte[] byteArray = File.ReadAllBytes(filePath);
            using MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(byteArray, 0, byteArray.Length);

            SimplifyMarkupSettings settings = new SimplifyMarkupSettings
            {
                RemoveComments = true,
                RemoveContentControls = true,
                RemoveEndAndFootNotes = true,
                RemoveFieldCodes = false,
                RemoveLastRenderedPageBreak = true,
                //  RemovePermissions = true,
                RemoveProof = true,
                RemoveRsidInfo = true,
                RemoveSmartTags = true,
                RemoveSoftHyphens = true,
                ReplaceTabsWithSpaces = true,
            };

            StringBuilder textBuilder = new StringBuilder();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(memoryStream, true))
            {
                _logger.LogDebug("Opening the word document from memoryStream.");

                MarkupSimplifier.SimplifyMarkup(wordDoc, settings);

                RevisionAccepter.AcceptRevisions(wordDoc);
                XElement root = wordDoc.MainDocumentPart.GetXDocument().Root;
                XElement body = root.LogicalChildrenContent().First();
                OutputBlockLevelContent(wordDoc, body, textBuilder);
            }
            return textBuilder.ToString();
        }

        private void OutputBlockLevelContent(WordprocessingDocument wordDoc, XElement blockLevelContentContainer, StringBuilder textBuilder)
        {
            _logger.LogDebug("Inside OutputBlockLevelContent");
            int imgCounter = 0;
            foreach (XElement blockLevelContentElement in blockLevelContentContainer.LogicalChildrenContent())
            {
                _logger.LogDebug($"blockLevelContentElement = {blockLevelContentElement.Name}");


                var pictures = new Paragraph(blockLevelContentElement.ToString()).Descendants<DocumentFormat.OpenXml.Wordprocessing.Picture>();
                foreach (var p in pictures)
                {
                    _logger.LogDebug($"imageParts.Count = {p}");
                    textBuilder.Append(AddPictureToHtml(p, wordDoc));
                }


                foreach (var child in blockLevelContentElement.Descendants().Where(o => o.Name.LocalName.Contains("drawing")))
                {
                    _logger.LogDebug($"child.Name.LocalName = {child.Name.LocalName}");

                    var imageParts = GetImagePartDetails(new Paragraph(blockLevelContentElement.ToString()));

                    if (imageParts != null && imageParts.Count != 0)
                    {
                        var imageBase64 = "";
                        var firstItem = imageParts.FirstOrDefault();
                        if (firstItem!= null && firstItem.Id != null && firstItem?.Filename != null)
                        {
                            _logger.LogDebug($"imageParts.Count = {imageParts.Count}");

                            _logger.LogDebug($"imageParts.FirstOrDefault().Id = {firstItem?.Id}  imageParts.FirstOrDefault().Filename ={firstItem.Filename}");

                            // string targetPath = Path.Combine(_examFile.OutPutImagesFolder, imgCounter.ToString() + ".png");
                            using (Stream source = wordDoc.MainDocumentPart.GetPartById(firstItem.Id).GetStream())
                            {
                                _logger.LogDebug($"Calling ImageConverter.ImageToBase64");
                                imageBase64 = ImageConverter.ImageToBase64(source);
                            }
                            // result.Append(string.Format("<img id=\"img{0}\" src=\"./images/{0}.png\" alt=\"Image {0}\" />", imgCounter.ToString()));
                            textBuilder.Append(
                                $"<img id=\"img{imgCounter++}\" src=\"data:image/png;base64,{imageBase64}\" {_regexSettings.ImageAttributeTag} />");
                            //Console.WriteLine(string.Format("<img id=\"img{0}\" src=\"data:image/png;base64,{1}\" {2} />", imgCounter++, imageBase64, Config.ImageAttributeTag));
                            imgCounter++;
                            _logger.LogDebug($"imgCounter = {imgCounter}");
                        }
                    }
                }

                string listItem;
                string text;
                if (blockLevelContentElement.Name == W.p)
                {
                    _logger.LogDebug($"(blockLevelContentElement.Name is W.p. Getting the text");

                    listItem = ListItemRetriever.RetrieveListItem(wordDoc, blockLevelContentElement);
                    text = blockLevelContentElement
                        .LogicalChildrenContent(W.r)
                        .LogicalChildrenContent(W.t)
                        .Select(t => (string)t)
                        .StringConcatenate();

                    textBuilder.Append(HttpUtility.HtmlEncode(listItem));
                    textBuilder.Append(HttpUtility.HtmlEncode(text));
                    textBuilder.Append(Environment.NewLine);

                    //logger.LogDebug($"{listItem} {text}");
                    continue;
                }
                // If element is not a paragraph, it must be a table.
                else if (blockLevelContentElement.Name == W.tbl)
                {
                    text = listItem = "";
                    _logger.LogDebug($"(blockLevelContentElement.Name is W.tbl. Parsing content to HTML Table by calling WordprocessingTableToHTML");

                    ToHtmlTable(new DocumentFormat.OpenXml.Wordprocessing.Table(blockLevelContentElement.ToString()), textBuilder);
                    continue;
                }
            }
        }


        string AddPictureToHtml(DocumentFormat.OpenXml.Wordprocessing.Picture pic, WordprocessingDocument doc)
        {
            string exit = "";
            DocumentFormat.OpenXml.Vml.Shape shape = pic.Descendants<DocumentFormat.OpenXml.Vml.Shape>().First();
            DocumentFormat.OpenXml.Vml.ImageData imageData =
                shape.Descendants<DocumentFormat.OpenXml.Vml.ImageData>().First();
            //style image
            string style = shape.Style;
            style = style.Replace("width:", "");
            style = style.Replace("height:", "");
            style = style.Replace('.', ',');
            style = style.Replace("pt", "");
            string[] arr = style.Split(';');
           // float styleW = float.Parse(arr[0]); //width picture
           // float styleH = float.Parse(arr[1]); //height picture
            string relationId = imageData.RelationshipId;
            var img = doc.MainDocumentPart.GetPartById(relationId);
            var uri = img.Uri; //path in file
            var fileName = uri.ToString().Split('/').Last(); //name picture
            // var fileWordMedia = img.GetStream(FileMode.Open);
            var imageBase64 = "";
            using (Stream source = img.GetStream(FileMode.Open))
            {
                _logger.LogDebug($"Calling ImageConverter.ImageToBase64");
                imageBase64 = ImageConverter.ImageToBase64(source);
            }

            exit =
                $"<img id=\"img{relationId}\" src=\"data:image/png;base64,{imageBase64}\" {_regexSettings.ImageAttributeTag}  />";

            // exit = String.Format("<img src=\"" + uri + "\" width=\"" + styleW + "\" heigth=\"" + styleH + "\" > ");
            return exit;
        }

        private List<dynamic> GetImagePartDetails(Paragraph par)
        {
            _logger.LogDebug("Inside GetImagePartDetails");

            var xpic = "";
            var xr = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

            var imageParts =
                       from graphic in par.Descendants<DocumentFormat.OpenXml.Drawing.Graphic>()
                       let graphicData = graphic.Descendants<DocumentFormat.OpenXml.Drawing.GraphicData>().FirstOrDefault()
                       let pic = graphicData.ElementAt(0)
                       let nvPicPrt = pic.ElementAt(0).FirstOrDefault()
                       let blip = pic.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault()
                       select new
                       {
                           Id = blip.GetAttribute("embed", xr).Value,
                           Filename = nvPicPrt.GetAttribute("name", xpic).Value
                       };

            _logger.LogDebug($"imageParts is: {imageParts}");

            return imageParts.ToList<dynamic>();
        }

        private void ToHtmlTable(DocumentFormat.OpenXml.Wordprocessing.Table node, StringBuilder textBuilder)
        {
            _logger.LogDebug($"Inside WordprocessingTableToHTML");

            textBuilder.Append("<table>");
            foreach (var row in node.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
            {
                textBuilder.Append("<tr>");
                foreach (var cell in row.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
                {
                    textBuilder.Append("<td>");
                    foreach (var para in cell.Descendants<Paragraph>())
                    {
                        textBuilder.Append(para.InnerText);
                    }
                    textBuilder.Append("</td>");
                }
                textBuilder.Append("</tr>");
            }
            textBuilder.Append("</table>");
            _logger.LogDebug($"Exiting WordprocessingTableToHTML");
        }
    }
}