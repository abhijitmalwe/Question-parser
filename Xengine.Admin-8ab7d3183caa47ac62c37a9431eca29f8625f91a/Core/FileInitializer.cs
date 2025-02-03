using System.IO;
using Xengine.Admin.Models;

namespace Xengine.Admin.Core
{
    public static class FileInitializer
    {
        private const string OutputFolderName = "Output";

        public static DocxFile Initialize(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return null;
            var docxFile = new DocxFile
            {
                InputFilePath = fileInfo.FullName.Trim(),
                InputFileNameNoExtension = fileInfo.Name.Replace(fileInfo.Extension, "").Trim()
            };

            if (fileInfo.Directory != null)
            {
                docxFile.OutputFolderPath =
                    Path.Combine(fileInfo.Directory.FullName, docxFile.InputFileNameNoExtension).Trim();
                docxFile.OutputFolderPath = Path.Combine(fileInfo.Directory.FullName, OutputFolderName).Trim();
                fileInfo.Directory.CreateSubdirectory(OutputFolderName);
            }

            docxFile.OutputFilePath = Path.Combine(docxFile.OutputFolderPath, docxFile.InputFileNameNoExtension + ".txt");
            docxFile.ErrorFilePath = Path.Combine(docxFile.OutputFolderPath, "Error.txt");
            docxFile.ExmFilePath = Path.Combine(docxFile.OutputFolderPath, docxFile.InputFileNameNoExtension + ".exm");
            //  _docxFile.OutPutImagesFolder = Path.Combine(_docxFile.OutputFolder, "images");
            // Directory.CreateDirectory(Path.Combine(_docxFile.OutputFolder, _docxFile.OutPutImagesFolder));
            //Delete if these files exist.
            File.Delete(docxFile.OutputFilePath);
            File.Delete(docxFile.ErrorFilePath);
            File.Delete(docxFile.ExmFilePath);

            return docxFile;

        }
    }
}