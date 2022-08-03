using CTI.TenantSales.PdfGenerator.Models;
using Rotativa;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace CTI.TenantSales.PdfGenerator.Services
{
    public class DailySalesReportToPdf
    {
        public async Task GeneratePdf(string generationFileNamePath, DailySalesReportModel dailySalesReport, Microsoft.AspNetCore.Mvc.ActionContext pageContext)
        {
            var dailySalesReportFile = new ViewAsPdf($"..\\Pdf\\DailySalesReport", dailySalesReport)
            {
                FileName = "DailySalesReport.pdf",
                PageOrientation = Orientation.Landscape,
                PageSize = Size.Folio,
                PageMargins = new Margins(5, 5, 5, 5),
                CustomSwitches = "--page-offset 0 --footer-center [page]/[topage] --footer-font-size 8",
            };
            var byteArray = await dailySalesReportFile.BuildFile(pageContext);
            using var fileStream = new FileStream(generationFileNamePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
        }
    }
}
