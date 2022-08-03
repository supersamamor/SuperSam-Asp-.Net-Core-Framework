using CTI.TenantSales.ExcelProcessor.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.ExcelProcessor.Services
{
    public class ExportSalesReportSummaryForIFCAFileService
    {
        public string Export(SalesSummaryForIFCA input, IList<SalesSummaryForIFCAItem> salesSummaryList)
        {
            var file = new ExportSalesSummaryForIFCAFile("ExcelFiles", "SalesReportForIFCA", "xlsx");

            using (var package = new ExcelPackage(file.File))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                //workSheet.Cells.LoadFromCollectionFiltered(salesSummaryList);

                #region Format the Excel File
                workSheet.Column(4).Style.Numberformat.Format = "mm/dd/yyyy";
                workSheet.Column(12).Style.Numberformat.Format = "#,##0.00";
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                workSheet.InsertRow(1, 1);
                workSheet.Cells["A1:A1"].Value = "Sales Summary For IFCA Upload Notice";
                workSheet.InsertRow(1, 1);

                var projectCodeLabel = input?.DatabaseCompanyProjectRoute == null ? "(All Projects)" : input.DatabaseCompanyProjectRoute;
                workSheet.Cells["A1:A1"].Value = @"Project Code : "
                                        + projectCodeLabel
                                        + @" \ Sales Summary from "
                                        + input?.DateFrom.ToString("MM/dd/yyyy") + @" to "
                                        + input?.DateTo.ToString("MM/dd/yyyy");
                workSheet.Cells["A1:L3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:L3"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                workSheet.Cells["A1:L1"].Merge = true;
                workSheet.Cells["A2:L2"].Merge = true;

                var endRow = salesSummaryList.Count + 3;
                workSheet.Cells["A1:L" + endRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["A1:L" + endRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["A1:L" + endRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["A1:L" + endRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                package.Save();
            }

            return file.DownloadUrl;
        }
    }
}
