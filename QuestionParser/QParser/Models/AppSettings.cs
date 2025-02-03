using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QParser.Admin.Models
{
  public class AppSettings
    {

        public string ConnectionString { set; get; }
        public string DbPassword { set; get; }
        public string FolderPathToProcess { set; get; }
        public string SqLiteConnectionString
        {
            get
            {
                var currentDir = Directory.GetCurrentDirectory();
                var dbPath = Path.Combine(currentDir, ConnectionString.Trim());
                return $"Data Source={dbPath};";
            }
            set { }
        }
        public bool IgnoreDuplicates { set; get; }
    }
}
