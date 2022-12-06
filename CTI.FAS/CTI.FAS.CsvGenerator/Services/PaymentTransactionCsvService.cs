using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.CsvGenerator.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CTI.FAS.CsvGenerator.Services
{
    public class PaymentTransactionCsvService
    {
        private readonly string _staticFolderPath;
        public PaymentTransactionCsvService(IConfiguration configuration)
        {
            _staticFolderPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        }
       
        public CsvDocumentModel Export(IList<PaymentTransactionState> input, string subfolder)
        {
            var csvDocument = new CsvDocumentModel($"ESettle{DateTime.Now:yyyyMMddHHmmss}.csv",
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
}
