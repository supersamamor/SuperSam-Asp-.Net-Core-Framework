using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.CsvGenerator.Models;
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
            var csvDocument = new CsvDocumentModel($"FAI_ACCOUNTENROLL_11162022_23{DateTime.Now:MMddyyyy}.csv",
                _staticFolderPath, subfolder, GlobalConstants.UploadFilesPath);
            var csv = new StringBuilder();
            //Append Header FAI_ACCOUNTENROLL_11162022_23
            csv.AppendLine(String.Format("{0,220}", "HDR,Participating Account Number,Participating Account Name,Participating Account Currency,Participating Account Type,Participating Account Reference Number,Participating Account TIN,Participating Account Address,Remarks"));
            //Append Detail
            foreach (var item in input)
            {
                var supplier = SanitizeSupplierName(item.Creditor!.PayeeAccountName);
                var accountNo = SanitizeAccountNo(item.PayeeAccountNumber);
                var creditorAccount = SanitizeCreditorCode(item.Creditor!.CreditorAccount);
                var newLine = String.Format("{0,3},{1,12},{2," + supplier.Length + "},{3,3},{4,2},{5," + creditorAccount.Length + "},{6,0},{7," + item.Creditor.PayeeAccountAddress.Length + "},{8,0}",
                                     "DTL",
                                     accountNo,
                                     supplier,
                                     "php",
                                     SanitizeAccountType(item.PayeeAccountType),
                                     creditorAccount,
                                     SanitizeTIN(item.Creditor.PayeeAccountTIN),
                                     SanitizeAddress(item.Creditor.PayeeAccountAddress),
                                     "");
                csv.AppendLine(newLine);
            }
            //Append Summary
            csv.AppendLine(String.Format("{0,3},{1,2},{2,0}",
                            "TLR",
                            input.Count,
                            ",,,,,,"));
         
            File.WriteAllText(csvDocument.CompleteFilePath, csv.ToString());
            return csvDocument;
        }
        private static string SanitizeAccountNo(string accountNo)
        {
            return accountNo.Replace("&nbsp;", "").Replace("&#241;", "ñ").Replace("&amp;", "&").Replace("-", "").Replace("NV", "").Replace("V", "").Trim().ToUpper();
        }
        private static string SanitizeSupplierName(string supplierName)
        {
            return supplierName.Replace("&nbsp;", "").Replace("&#209;", "Ñ").Replace("&#241;", "ñ").Replace("&#39;", "'").Replace("&amp;", "&").Trim().ToUpper();
        }
        private static string SanitizeCreditorCode(string? creditorCode)
        {
            if (creditorCode == null)
            {
                return "";
            }
            return creditorCode!.Replace("&nbsp;", "").Replace("&#241;", "ñ").Replace("&amp;", "&").Replace("-", "").Replace("NV", "").Replace("V", "").Trim();
        }
        private static string SanitizeAccountType(string? accountType)
        {
            if (accountType == AccountType.Savings)
            {
                return "SA";
            }
            else if (accountType == AccountType.Checking)
            {
                return "CA";
            }
            return "";
        }
        private static string SanitizeAddress(string address)
        {
            return address.Replace(",", " ").ToUpper();
        }
        private static string SanitizeTIN(string? tin)
        {
            if (tin == null)
            {
                return "";
            }
            return tin.Replace("&nbsp;", "").Replace("&#241;", "ñ").Replace("&amp;", "&").Replace("-", "").Replace("NV", "").Replace("V", "").Trim();
        }
    }
}
