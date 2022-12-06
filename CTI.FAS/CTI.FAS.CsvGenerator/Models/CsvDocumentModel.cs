using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.FAS.CsvGenerator.Models
{
    public class CsvDocumentModel
    {
        public CsvDocumentModel(string fileName, string staticFolderPath, string subFolderPath, string staticFolder)
        {
            var folderPath = staticFolderPath + "\\" + subFolderPath;
            this.FileName = fileName;
            this.CompleteFilePath = folderPath + "\\" + this.FileName;
            this.FileUrl = "../../" + staticFolder + "/" + subFolderPath + "/" + this.FileName;
            bool folderPathExists = Directory.Exists(folderPath);
            if (!folderPathExists)
                Directory.CreateDirectory(folderPath);
        }
        public string FileName { get; set; } = "";
        public string CompleteFilePath { get; private set; } = "";
        public string FileUrl { get; private set; } = "";
    }
}
