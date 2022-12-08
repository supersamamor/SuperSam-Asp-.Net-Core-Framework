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

        public CsvDocumentModel Export(IList<PaymentTransactionState> input, string? entityCode, int? batchNo, string subfolder)
        {
            if (string.IsNullOrEmpty(entityCode))
            {
                entityCode = "00";
            }
            if (batchNo == null)
            {
                batchNo = 1;
            }
            var csvDocument = new CsvDocumentModel($"ESETTLE_{entityCode}_{DateTime.Now:MMddyyyy}_{batchNo}.csv",
                _staticFolderPath, subfolder, GlobalConstants.UploadFilesPath);
            var csv = new StringBuilder();
            //Append Header 
            csv.AppendLine(String.Format("{0,380}", "HDR,Participating Account Number,Participating Account Name,Participating Reference Number,Transaction Amount,Transaction Reference Number,Print EWT?,Record Type,ATC Code,Tax Period From,Tax Period To,Amount of Income Payments (1st Quarter),Amount of Income Payments (2nd Quarter),Amount of Income Payment (3rd Quarter),Total Amount of Income Payments,Tax Withheld for the Quarter"));
            //Append Detail
            foreach (var item in input)
            {
                var supplier = SanitizeSupplierName(item.EnrolledPayee!.Creditor!.PayeeAccountName);
                var accountNo = SanitizeAccountNo(item.EnrolledPayee!.PayeeAccountNumber);
                var creditorAccount = SanitizeCreditorCode(item.EnrolledPayee!.Creditor!.CreditorAccount);         
                var newLine = String.Format("{0,3},{1,12},{2," + supplier.Length + "},{3," + accountNo.Length + "},{4," + item.DocumentAmount.ToString().Length + "},{5," + item.DocumentNumber.Length + "},{6,2},{7,0}",
                                        "DTL", accountNo, supplier,
                                        creditorAccount, item.DocumentAmount, item.DocumentNumber, "No", ",,,,,,,,,");
                csv.AppendLine(newLine);
            }
            //Append Summary
            var totalAmount = input.Sum(l => l.DocumentAmount);
            csv.AppendLine(String.Format("{0,3},{1," + input.Count.ToString().Length + "},{2," + totalAmount.ToString().Length + "},{3,0}",
                             "TLR",
                             input.Count,
                             totalAmount,
                             ",,,,,,,,,,,,,"));

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
