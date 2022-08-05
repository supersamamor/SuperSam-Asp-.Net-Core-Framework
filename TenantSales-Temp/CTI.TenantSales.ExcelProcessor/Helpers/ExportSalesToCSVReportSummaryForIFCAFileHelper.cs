using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.ExcelProcessor.Models;
using System.Text;
namespace CTI.TenantSales.ExcelProcessor.Helper
{
    public static class ExportSalesToCSVReportSummaryForIFCAFileHelper
    {
        public static string Export(string staticPath, string fullFilePath, DateTime dateFrom, DateTime dateTo, IList<TenantState> tenantList)
        {
            var fileName = "SalesReportForIFCA-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";
            var csv = new StringBuilder();
            var projectList = tenantList.Select(l=>l.ProjectId).Distinct().ToList();     
            var projectCodeLabel = (projectList.Count > 1) ? "(All Projects)" : tenantList.Select(l => l.Project!.DisplayDescription).FirstOrDefault();
            csv.AppendLine(@"Project Code : "
                                    + projectCodeLabel
                                    + @" \ Sales Summary from "
                                    + dateFrom.ToString("MM/dd/yyyy") + @" to "
                                    + dateTo.ToString("MM/dd/yyyy"));
            csv.AppendLine("REMINDER: CUT THE FIRST THREE (3) ROWS BEFORE UPLOADING IN IFCA.");
            csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        "TenantCode", "ProjectCode", "EntityCode", "SalesDate", "SalesCategory",
                         "CurrencyCode", "CurrencyRate", "Status", "AuditDate", "AuditUser", "TenantName", "TotalMonthlySales"));
            var salesSummaryList = ConvertToSummaryList(dateTo, tenantList);
            foreach (var item in salesSummaryList)
            {
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        item.TenantCode, item.ProjectCode, "=\"" + item.EntityCode + "\"", item.SalesDate, item.SalesCategory,
                         "", "", "", "", "", item.TenantName.Replace(",", ""), item.TotalMonthlySales);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(Path.Combine(fullFilePath, fileName), csv.ToString());
            return staticPath + "\\" + fileName;
        }
        private static IList<SalesSummaryForIFCAItem> ConvertToSummaryList(DateTime dateTo, IList<TenantState> tenantList)
        {
            var monthlySummaryList = new List<SalesSummaryForIFCAItem>();
            var salesList = tenantList.SelectMany(l => l.TenantPOSList!.SelectMany(l => l.TenantPOSSalesList!)).ToList();
            var results = (from sa in salesList
                           select new
                           {
                               EntityCode = sa.TenantPOS!.Tenant!.Project!.Company!.Code,
                               ProjectCode = sa.TenantPOS!.Tenant!.Project!.Code,
                               TenantCode = sa.TenantPOS.Tenant.Code,
                               TenantName = sa.TenantPOS.Tenant.Name,
                               sa.TenantPOS!.Tenant!.ProjectId,
                               TenantId = sa.TenantPOS.Tenant.Id,
                               sa.SalesCategory
                           }).Distinct();
            var salesSummaryDate = new DateTime(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month));
            foreach (var item in results)
            {
                var totalNetSales = salesList.Where(l => l.TenantPOS!.TenantId == item.TenantId & l.SalesCategory == item.SalesCategory).Sum(l => l.TotalNetSales);
                monthlySummaryList.Add(new SalesSummaryForIFCAItem(item.EntityCode,
                                                item.ProjectCode,
                                                item.TenantCode, salesSummaryDate,
                                                item.SalesCategory,
                                                item.TenantName,
                                                totalNetSales));
            }
            return monthlySummaryList;
        }
    }
}
