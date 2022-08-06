using CTI.TenantSales.PdfGenerator.Models;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace CTI.TenantSales.PdfGenerator.Helper
{
    public static class SalesGrowthReportToPdf
    {
        public static async Task<string> GeneratePdf(string staticPath, string fullFilePath, SalesGrowthDataModel salesGrowth, Microsoft.AspNetCore.Mvc.ActionContext pageContext)
        {
            var fileName = "SalesGrowthPerformanceReport-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            var dailySalesReportFile = new ViewAsPdf($"..\\SalesGrowth\\Pdf\\SalesGrowthPerformanceReport", salesGrowth)
            {
                FileName = fileName,
                PageOrientation = Orientation.Landscape,
                PageSize = Size.A3,
                PageMargins = new Margins(5, 5, 5, 5),
                CustomSwitches = "--page-offset 0 --footer-center [page]/[topage] --footer-font-size 8",
            };
            var byteArray = await dailySalesReportFile.BuildFile(pageContext);
            using var fileStream = new FileStream(Path.Combine(fullFilePath, fileName), FileMode.Create, FileAccess.Write);
            {
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();
            }
            return staticPath + "\\" + fileName;
        }
    }
}
