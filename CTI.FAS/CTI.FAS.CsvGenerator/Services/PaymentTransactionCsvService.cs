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
            if (input?.FirstOrDefault()?.PaymentType == PaymentType.ESettle)
            {
                return ESettle(input, entityCode, batchNo, subfolder);
            }
            else
            {
                return CheckPrepare(input!, entityCode, batchNo, subfolder);
            }
        }
        private CsvDocumentModel CheckPrepare(IList<PaymentTransactionState> input, string? entityCode, int? batchNo, string subfolder)
        {
            var csvDocument = new CsvDocumentModel($"FUNDS_{entityCode}_{DateTime.Now:MMddyyyy}_{batchNo}.csv",
             _staticFolderPath, subfolder, GlobalConstants.UploadFilesPath);
            var csv = new StringBuilder();
            //Append Header 
            csv.AppendLine(String.Format("{0,517}", "HDR,Pick-up or Delivery,Pick-up Store,Pick-up Representative,Authorized Representative Name,Authorized Representative ID,Delivery Corporation Branch,Check Date," +
                                          "Check Amount,Payee Name,Is Payee Account Only?,Remarks,Signatories,Signatory Name1,Signatory Name2,Signatory Name3,Print EWT?,Record Type,ATC Code,Tax Period From,Tax Period To," +
                                          "Amount of Income Payments (1st Quarter),Amount of Income Payments (2nd Quarter),Amount of Income Payments (3rd Quarter),Total Amount of Income Payments," +
                                          "Tax Withheld for the Quarter"));
            //Append Detail
            foreach (var item in input)
            {
                var remarks = "";

                var printEWT = "No";
                var recordType = "";
                var atcCode = "";
                var taxPeriodFrom = "";
                var taxPeriodTo = "";
                var amtIncomePay1stQtr = "";
                var amtIncomePay2ndQtr = "";
                var amtIncomePay3rdQtr = "";
                var taxWithheldQtr = "";
                var pickupStore = "";
                var pickupRep = "";
                var authorizedRepName = "";
                var authorizedRepID = "";
                var isPayeeAcctOnly = "YES";
                var supplier = SanitizeSupplierName(item.EnrolledPayee!.Creditor!.PayeeAccountName);
                var accountNo = SanitizeAccountNo(item.EnrolledPayee!.PayeeAccountNumber);
                var creditorAccount = SanitizeCreditorCode(item.EnrolledPayee!.Creditor!.CreditorAccount);
                var checkDate = item.TransmissionDate != null ? ((DateTime)item.TransmissionDate).ToString("MM/dd/yyyy").Trim() : "";
                var checkAmount = item.DocumentAmount;
                var signatories = SanitizeForCsv(item.EnrolledPayee.Company?.SignatoryType);
                var signatoryName1 = SanitizeForCsv(item.EnrolledPayee.Company?.Signatory1);
                var signatoryName2 = SanitizeForCsv(item.EnrolledPayee.Company?.Signatory2);
                var signatoryName3 = "";
                var deliveryCorpBranch = SanitizeForCsvToUpper(item.EnrolledPayee.Company?.DeliveryCorporationBranch);
                var newLine = String.Format("{0,3},{1," + item.EnrolledPayee!.Creditor!.DeliveryOptions!.Length + "},{2," + pickupStore.Length + "}," +
                                          "{3," + pickupRep.Length + "},{4," + authorizedRepName.Length + "},{5," + authorizedRepID.Length + "}," +
                                          "{6," + deliveryCorpBranch.Length + "},{7," + checkDate.Length + "},{8," + checkAmount.ToString().Length + "}," +
                                          "{9," + supplier.Length + "},{10," + isPayeeAcctOnly.Length + "},{11," + remarks.Length + "}," +
                                          "{12," + signatories.Length + "},{13," + signatoryName1.Length + "},{14," + signatoryName2.Length + "}," +
                                          "{15," + signatoryName3.Length + "},{16," + printEWT.Length + "},{17," + recordType.Length + "}," +
                                          "{18," + atcCode.Length + "},{19," + taxPeriodFrom.Length + "},{20," + taxPeriodTo.Length + "}," +
                                          "{21," + amtIncomePay1stQtr.Length + "},{22," + amtIncomePay2ndQtr.Length + "},{23," + amtIncomePay3rdQtr.Length + "},{24," + taxWithheldQtr.Length + "}",
                                          "DTL", item.EnrolledPayee!.Creditor!.DeliveryOptions!.Length, pickupStore, pickupRep,
                                          authorizedRepName, authorizedRepID, deliveryCorpBranch,
                                          checkDate, checkAmount, supplier,
                                          isPayeeAcctOnly, remarks, signatories,
                                          signatoryName1, signatoryName2, signatoryName3,
                                          printEWT, recordType, atcCode,
                                          taxPeriodFrom, taxPeriodTo, amtIncomePay1stQtr,
                                          amtIncomePay2ndQtr, amtIncomePay3rdQtr, taxWithheldQtr);
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
        private CsvDocumentModel ESettle(IList<PaymentTransactionState> input, string? entityCode, int? batchNo, string subfolder)
        {
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
        private static string SanitizeForCsv(string? text)
        {
            if (text == null)
            {
                return "";
            }
            return text.Replace(",", " ");
        }
        private static string SanitizeForCsvToUpper(string? text)
        {
            if (text == null)
            {
                return "";
            }
            return text.Replace(",", " ").ToUpper();
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
