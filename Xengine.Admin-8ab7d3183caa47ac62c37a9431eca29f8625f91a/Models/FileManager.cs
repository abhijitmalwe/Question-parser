using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xengine.Admin.Models
{
    public class FileManager
    {
        private List<string> _files = new List<string>();
        public string FilePath { get; set; }
        public string FileText { get; set; }

        public string Read()
        {
            return File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8).Trim() : null;
        }

        public void Write()
        {
            File.WriteAllText(FilePath, FileText);
        }

        public void WriteAppend()
        {
            using StreamWriter sw = (File.Exists(FilePath)) ? File.AppendText(FilePath) : File.CreateText(FilePath);
            sw.Write(FileText);
        }
    }
}