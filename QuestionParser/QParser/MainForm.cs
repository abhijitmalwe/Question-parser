using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Windows.Forms;
using QParser.Admin.Core;
using QParser.Admin.Models;

namespace QParser.Admin
{
    public partial class MainForm : Form
    {
        private DocxFile _examFile;
        private readonly ILogger<MainForm> _logger;
        private readonly IWordprocessor _wordprocessor;
        private readonly IRegexParser _regexExamParser;
        private readonly IDbManager _dbManager;

        private readonly AppSettings _appSettings;

        private string htmlTemplate = "<!DOCTYPE html><html><head><style>body{padding:10px; max-width: 100%; font-family:arial; font-szie:12pt;} img{max-width: 90%;} pre{overflow-x: auto; white-space: pre-wrap;white-space: -moz-pre-wrap;white-space: -pre-wrap;white-space: -o-pre-wrap;word-wrap: break-word;}</style></head><body><pre>{0}</pre> </body></html>";

        public MainForm(ILoggerFactory loggerFactory, IWordprocessor wp, IRegexParser regexP, IDbManager dbMgr, AppSettings appSettings)
        {
            _logger = loggerFactory.CreateLogger<MainForm>();
            _wordprocessor = wp;
            _regexExamParser = regexP;
            _dbManager = dbMgr;
            _appSettings = appSettings;

            _logger.LogTrace("LogTrace");
            _logger.LogDebug("LogDebug");
            _logger.LogInformation("LogInformation");
            _logger.LogWarning("LogWarning");
            _logger.LogError("LogError");
            _logger.LogCritical("LogCritical");

            InitializeComponent();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            InitUi();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document == null) return;
            if (webBrowser1.Document.Body == null) return;
            if (webBrowser1.Document.Body.Parent != null)
                ClientSize = new Size(
                    webBrowser1.Document.Body.Parent.ScrollRectangle.Width,
                    webBrowser1.Document.Body.Parent.ScrollRectangle.Height);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event

            if (!sendingWorker.CancellationPending)//At each iteration of the loop,
            {
                backgroundWorker1.ReportProgress(10, "Reading file...");

                _logger.LogDebug($"Process Folder? {chProcessAll.Checked}");

                //If batch Process
                if (chProcessAll.Checked)
                {
                    lblMsg.Visible = true;

                    foreach (string f in Directory.EnumerateFiles(_appSettings.FolderPathToProcess, "*.docx", SearchOption.AllDirectories))
                    {
                        _logger.LogDebug($"Batch - Reading file: {f}");

                        _examFile = FileInitializer.Initialize(f.Trim());
                        lblMsg.Text = ("Reading input file...");
                        if (_examFile != null)
                        {
                            _logger.LogDebug($"Batch - {_examFile} is not NULL. Parsing file now. By calling  Wordprocessor.Process(examFile.InputFilePath)");

                            lblMsg.Text = ("Parsing file...");

                            // Convert the Docx to string and get the content back.
                            _logger.LogDebug($"Batch - Convert the Docx to string and get the content back.");

                            _examFile.Content = _wordprocessor.Process(_examFile.InputFilePath);
                            _logger.LogDebug($"Batch - Word is converted to Text file. Saving to DB now.");

                            SaveToDb(_examFile);
                            _logger.LogDebug($"Files is saved to DB.");
                        }
                    }
                    //Process the folder
                    return;
                }

                try
                {
                    webBrowser1.DocumentText = "";

                    _logger.LogDebug($"Processing single file.");

                    _examFile = FileInitializer.Initialize(tbxFile.Text.Trim());
                    _logger.LogDebug($"File has been parsed.");

                    //lblMsg.Visible = true;
                    backgroundWorker1.ReportProgress(12, "Reading input file...");
                    _logger.LogDebug($"Background process has been started.");

                    // lblMsg.Text = ("Reading input file...");
                    if (_examFile != null)
                    {
                        _logger.LogDebug($"examFile object is not NULL.");

                        // lblMsg.Text = ("Parsing file...");

                        backgroundWorker1.ReportProgress(15, "Parsing file...");

                        _logger.LogDebug($"Converting single Docx to string and get the content back.");

                        // Convert the Docx to string and get the content back.
                        _examFile.Content = _wordprocessor.Process(_examFile.InputFilePath);

                        _logger.LogDebug($"Word is converted to Text file. Saving to DB now.");

                        backgroundWorker1.ReportProgress(45, "Generating the .EXM file. Please wait...");

                        webBrowser1.DocumentText = htmlTemplate.Replace("{0}", _examFile.Content);

                        // lblMsg.Text = ("Generating the .EXM file. Please wait...");
                        // lblMsg.Visible = true;
                        backgroundWorker1.ReportProgress(60, "Generating the .EXM file. Please wait...");

                        _logger.LogDebug($"Saving file to DB.");

                        if (SaveToDb(_examFile))
                        {
                            _logger.LogDebug($"File saved to DB.");
                           File.Delete(_examFile.OutputFilePath);
                            backgroundWorker1.ReportProgress(100, "Done!");
                        }
                        else
                        {
                            _logger.LogDebug($"There was an error saving the file to DB.");
                            MessageBox.Show("There are errors in this file. Please fix them.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoadErrorFile();
                            e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true

                            backgroundWorker1.ReportProgress(100, "Error!");

                        }


                    }
                    else
                    {
                        _logger.LogDebug($"examFile object is NULL.");

                        backgroundWorker1.ReportProgress(0, "File does not exist!");
                        e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true

                        MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  
                }
                catch (Exception ex)
                {
                    _logger.LogError("{0}", ex.StackTrace);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblMsg.Text = e.UserState?.ToString();
            lblMsg.Visible = true;
            //lblMsg.ForeColor = System.Drawing.Color.Red;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Cancel BackgroundWorker
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
            if (e.Cancelled)
            {
                lblMsg.Text = "Task Cancelled.";
                lblMsg.Visible = true;
                btnCancel.Enabled = false;
                btnOpenDir.Enabled = true;
                lblMsg.ForeColor = Color.FromArgb(238, 15, 15);
            }
            else if (e.Error != null)
            {
                _logger.LogError($"{e.Error.StackTrace}");
                MessageBox.Show(e.Error.Message);
                lblMsg.ForeColor = Color.FromArgb(238, 15, 15);
            }
            else
            {
                lblMsg.Text = "Task Completed!";
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.FromArgb(11, 114, 11);
                progressBar1.Value = 100;
                btnCancel.Enabled = false;
                btnOpenDir.Enabled = true;
            }
        }

        /// <summary>
        ///  Handle the btnOpen Click event to load an Word file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            _logger.LogDebug($" BtnOpen_Click clicked.");
            SelectWordFile();
        }

        /// <summary>
        /// Show an OpenFileDialog to select a Word document.
        /// </summary>
        /// <returns>
        /// The file name.
        /// </returns>
        private string SelectWordFile()
        {
            _logger.LogDebug($"Inside SelectWordFile()");

            InitUi();
            string fileName = null;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Word document (*.docx)|*.docx";
                dialog.InitialDirectory = Environment.CurrentDirectory;

                // Restore the directory before closing
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _logger.LogDebug($"File Name is: {dialog.FileName}");
                    fileName = dialog.FileName;
                    tbxFile.Text = dialog.FileName;
                }
            }

            return fileName;
        }

        /// <summary>
        /// Get Plain Text from Word file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            _logger.LogDebug($"Button Start clicked.");

            btnCancel.Enabled = true;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private bool SaveToDb(DocxFile examFile)
        {
            _logger.LogDebug($"Inside the SaveToDb function.");

            //Save the extracted content to a text file.This file is not used. It is for reference only.
            FileManager fManager = new FileManager()
            {
                FilePath = examFile.OutputFilePath,
                FileText = examFile.Content
            };
            fManager.Write();

            backgroundWorker1.ReportProgress(70, "Generating .EXM file...");

            _logger.LogDebug($"RegEx parsing starts here.");

            // Start parsing
            //_regexExamParser.Content = examFile.Content;
           // _regexExamParser.ExamName = examFile.InputFileNameNoExtension;

            fManager.FilePath = examFile.ErrorFilePath;
            //Send the content of the file as text for parsing using RegX. Return object is an Exam with everything.
            var exam = _regexExamParser.Parse(fManager, examFile.Content, examFile.InputFileNameNoExtension, _appSettings.IgnoreDuplicates);
            _logger.LogDebug($"RegEx parsing DONE.");

            _logger.LogDebug($"Going to save {exam.Name}  to Sqlite DB now.");
            _logger.LogDebug($"exam.Questions.Count() = {exam.Questions.Count()} ");
            _logger.LogDebug($"exam.HasErro = {exam.HasError} ");

           
            if (exam.Questions.Any() && exam.HasError == false)
            {
                _logger.LogDebug($"{exam.Name} is being saved to Sqlite DB now.");

                _dbManager.Save(exam, examFile, backgroundWorker1);
                _logger.LogDebug($"{exam.Name} saved to Sqlite DB.");

                return true;
            }
            else
            {
                _logger.LogDebug($"exam object has issues. It is either null, or has no questions. Or it has errors.");

                return false;
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            InitUi();
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            _logger.LogDebug($"Inside the Form1_DragDrop function.");

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length == 1)
            {
                tbxFile.Text = files[0];
                _logger.LogDebug($"Inside the Form1_DragDrop file is: {files[0]}.");
                webBrowser1.DocumentText = "";
            }
            else
            {
                MessageBox.Show("Error loading the file. Make sure the file is not in use by another application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", _examFile.OutputFolderPath);
        }

        private void InitUi()
        {
            _logger.LogDebug($"Inside InitUi()");

            btnOpenDir.Enabled = false;
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Green;
            lblMsg.Visible = false;
            btnShowErr.Enabled = false;
            btnShowText.Enabled = false;
            tbxFile.Text = "";
            webBrowser1.DocumentText = "";
            progressBar1.Value = 0;
        }

        private void LoadError(object sender, EventArgs e)
        {
            _logger.LogDebug($"Inside the LoadError().");
            LoadErrorFile();
        }

      void  LoadErrorFile()
        {

            if (btnShowErr.InvokeRequired)
            {
                btnShowErr.Invoke(new Action(LoadErrorFile));
                return;
            }


            if (!File.Exists(_examFile.ErrorFilePath))
            {
                _logger.LogDebug($"File {_examFile.ErrorFilePath} DOES NOT exist.");

                webBrowser1.DocumentText = htmlTemplate.Replace("{0}", "There is no error in this file.");
                btnShowText.Enabled = false;
                btnShowErr.Enabled = true;
                return;
            }

            var html = File.ReadAllText(_examFile.ErrorFilePath);
            webBrowser1.DocumentText = htmlTemplate.Replace("{0}", html);
            btnShowText.Visible = true;
            btnShowText.Enabled = true;
            btnShowErr.Enabled = false;
        }

        private void LoadTextFile(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = htmlTemplate.Replace("{0}", _examFile.Content);

            btnShowText.Enabled = false;
            btnShowErr.Enabled = true;
        }
       
    }
}