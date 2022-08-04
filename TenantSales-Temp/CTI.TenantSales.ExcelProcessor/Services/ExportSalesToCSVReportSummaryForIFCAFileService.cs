using CTI.TenantSales.ExcelProcessor.Models;
using System.Text;
namespace CTI.TenantSales.ExcelProcessor.Services
{
    public static class ExportSalesToCSVReportSummaryForIFCAFileService
    {
        public static string Export(SalesSummaryForIFCA input, IList<SalesSummaryForIFCAItem> salesSummaryList)
        {
            var file = new ExportSalesSummaryForIFCAFile("ExcelFiles", "SalesReportForIFCA", "csv");
            var csv = new StringBuilder();
            var projectCodeLabel = input.DatabaseCompanyProjectRoute ?? "(All Projects)";
            csv.AppendLine(@"Project Code : "
                                    + projectCodeLabel
                                    + @" \ Sales Summary from "
                                    + input.DateFrom.ToString("MM/dd/yyyy") + @" to "
                                    + input.DateTo.ToString("MM/dd/yyyy"));
            csv.AppendLine("REMINDER: CUT THE FIRST THREE (3) ROWS BEFORE UPLOADING IN IFCA.");
            csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        "TenantCode", "ProjectCode", "EntityCode", "SalesDate", "SalesCategory",
                         "CurrencyCode", "CurrencyRate", "Status", "AuditDate", "AuditUser", "TenantName", "TotalMonthlySales"));
            foreach (var item in salesSummaryList)
            {
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        item.TenantCode, item.ProjectCode, "=\"" + item.EntityCode + "\"", item.SalesDate, item.SalesCategory,
                         "", "", "", "", "", item.TenantName.Replace(",", ""), item.TotalMonthlySales);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(file.File.FullName, csv.ToString());
            return file.DownloadUrl;
        }
    }
}
