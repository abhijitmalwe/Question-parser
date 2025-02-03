namespace QParser.Admin.Models
{
    public class DocxFile
    {
        /// <summary>
        /// Full path of the output text file
        /// </summary>
        public string OutputFilePath { get; set; }

        /// <summary>
        /// The input file name without extension i.e. DEV-001
        /// </summary>
        public string InputFileNameNoExtension { get; set; }

        /// <summary>
        /// Full path of the Input File
        /// </summary>
        public string InputFilePath { get; set; }

        /// <summary>
        /// Full path to Output Folder which holds all the files
        /// </summary>
        public string OutputFolderPath { get; set; }

        /// <summary>
        /// Full path to Images folder
        /// </summary>
        public string OutPutImagesFolderPath { get; set; }

        /// <summary>
        /// Total number of Questions found in a input file
        /// </summary>
        public int TotalQuestions { get; set; }

        public string Content { get; set; }
        public string ErrorFilePath { get; set; }
        public string ExmFilePath { get; internal set; }
    }
}