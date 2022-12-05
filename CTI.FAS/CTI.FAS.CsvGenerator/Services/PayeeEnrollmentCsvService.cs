using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CTI.FAS.CsvGenerator.Services
{
    public class PayeeEnrollmentCsvService
    {
        private readonly string _staticFolderPath;
        public PayeeEnrollmentCsvService(IConfiguration configuration)
        {
            _staticFolderPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        }
       
        public CsvDocumentModel Export(IList<EnrolledPayeeState> input, string subfolder)
        {
            var csvDocument = new CsvDocumentModel($"AccountEnrollment{DateTime.Now:yyyyMMddHHmmss}.csv",
                _staticFolderPath, subfolder, GlobalConstants.UploadFilesPath);
            var csv = new StringBuilder();
            foreach (var item in input)
            {
                var newLine = string.Format("{0}",
                        item.Id);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(csvDocument.CompleteFilePath, csv.ToString());
            return csvDocument;
        }
    }
    public class CsvDocumentModel
    {
        public CsvDocumentModel(string fileName, string staticFolderPath, string subFolderPath, string staticFolder)
        {
            this.FileName = fileName;
            this.CompleteFilePath = staticFolderPath + "\\" + subFolderPath + "\\" + this.FileName;
            this.FileUrl = "\\" + staticFolder + "\\" + subFolderPath + "\\" + this.FileName;            
        }
        public string FileName { get; set; } = "";
        public string CompleteFilePath { get; private set; } = "";
        public string FileUrl { get; private set; } = "";       
    }
}
