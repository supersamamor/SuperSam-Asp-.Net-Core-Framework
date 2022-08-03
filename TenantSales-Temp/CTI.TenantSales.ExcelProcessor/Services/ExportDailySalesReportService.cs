using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.ExcelProcessor.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CTI.TenantSales.ExcelProcessor.Services
{
    public class ExportDailySalesReportService
    {
        public string Export(DateTime dateFrom, DateTime dateTo, IList<TenantPOSSalesState> salesList)
        {
            var file = new ExportDailySalesReport("ExcelFiles", "DailySalesReport", "xlsx");

            using (var package = new ExcelPackage(file.File))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                #region Header
                workSheet.Cells["A1:A1"].Value = @"Daily Sales Report from "
                                     + dateFrom.ToString("MM/dd/yyyy") + @" to "
                                     + dateTo.ToString("MM/dd/yyyy");
                workSheet.Cells["A1:X1"].Merge = true;
                workSheet.Column(6).Style.Numberformat.Format = "mm/dd/yyyy";
                workSheet.Column(8).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(9).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(10).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(11).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(12).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(13).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(14).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(15).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(16).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(17).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(18).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(19).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(20).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(21).Style.Numberformat.Format = "#,##0.00";
                workSheet.Column(22).Style.Numberformat.Format = "#,##0.00";
                workSheet.Cells["A2"].Value = "Project";
                workSheet.Cells["B2"].Value = "Tenant Code";
                workSheet.Cells["C2"].Value = "Tenant Name";
                workSheet.Cells["D2"].Value = "POS No";
                workSheet.Cells["E2"].Value = "Day";
                workSheet.Cells["F2"].Value = "Sales Date";
                workSheet.Cells["G2"].Value = "Sales Category";
                workSheet.Cells["H2"].Value = "Old Accumulated Sales";
                workSheet.Cells["I2"].Value = "New Accumulated Sales";
                workSheet.Cells["J2"].Value = "Taxable Sales Amount";
                workSheet.Cells["K2"].Value = "Non Taxable Sales Amount";
                workSheet.Cells["L2"].Value = "Service Charge";
                workSheet.Cells["M2"].Value = "Tax Amount";
                workSheet.Cells["N2"].Value = "Total Sales";
                workSheet.Cells["O2"].Value = "Refund Amount";
                workSheet.Cells["P2"].Value = "Adjustment Amount";
                workSheet.Cells["Q2"].Value = "Void Amount";
                workSheet.Cells["R2"].Value = "Senior Discount";
                workSheet.Cells["S2"].Value = "Promo Discount";
                workSheet.Cells["T2"].Value = "Other Discount";
                workSheet.Cells["U2"].Value = "Count of All Transaction";
                workSheet.Cells["V2"].Value = "Count of Sales Transaction";
                workSheet.Cells["W2"].Value = "Status";
                workSheet.Cells["X2"].Value = "Failed Remarks";

                workSheet.Cells["A1:X2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A1:X2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:X2"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                workSheet.Cells["A1:X2"].Style.Font.Bold = true;
                #endregion
                #region Fill Values
                var sheet1Row = 3;
                foreach (var salesItem in salesList)
                {
                    if (salesItem.TotalNetSales != 0)
                    {
                        workSheet.Cells[sheet1Row, 1].Value = salesItem?.TenantPOS?.Tenant?.Project?.Name;
                        workSheet.Cells[sheet1Row, 2].Value = salesItem?.TenantPOS?.Tenant?.Code;
                        workSheet.Cells[sheet1Row, 3].Value = salesItem?.TenantPOS?.Tenant?.Name;
                        workSheet.Cells[sheet1Row, 4].Value = salesItem?.TenantPOS?.Code;
                        workSheet.Cells[sheet1Row, 5].Value = salesItem?.SetDayNumber(dateFrom);
                        workSheet.Cells[sheet1Row, 6].Value = salesItem?.SalesDate;
                        workSheet.Cells[sheet1Row, 7].Value = salesItem?.SalesCategory;
                        workSheet.Cells[sheet1Row, 8].Value = salesItem?.OldAccumulatedTotal;
                        workSheet.Cells[sheet1Row, 9].Value = salesItem?.NewAccumulatedTotal;
                        workSheet.Cells[sheet1Row, 10].Value = salesItem?.TaxableSalesAmount;
                        workSheet.Cells[sheet1Row, 11].Value = salesItem?.NonTaxableSalesAmount;
                        workSheet.Cells[sheet1Row, 12].Value = salesItem?.TotalServiceCharge;
                        workSheet.Cells[sheet1Row, 13].Value = salesItem?.TotalTax;
                        workSheet.Cells[sheet1Row, 14].Value = salesItem?.TotalNetSales;
                        workSheet.Cells[sheet1Row, 15].Value = salesItem?.RefundDiscount;
                        workSheet.Cells[sheet1Row, 16].Value = salesItem?.AdjustmentAmount;
                        workSheet.Cells[sheet1Row, 17].Value = salesItem?.VoidAmount;
                        workSheet.Cells[sheet1Row, 18].Value = salesItem?.SeniorCitizenDiscount;
                        workSheet.Cells[sheet1Row, 19].Value = salesItem?.PromoDiscount;
                        workSheet.Cells[sheet1Row, 20].Value = salesItem?.OtherDiscount;
                        workSheet.Cells[sheet1Row, 21].Value = salesItem?.NoOfTransactions;
                        workSheet.Cells[sheet1Row, 22].Value = salesItem?.NoOfSalesTransactions;
                        workSheet.Cells[sheet1Row, 23].Value = salesItem?.ValidationStatus == Convert.ToInt32(Core.Constants.ValidationStatusEnum.Passed) ? "Passed" : "Failed";
                        workSheet.Cells[sheet1Row, 24].Value = salesItem?.ValidationRemarks;
                        sheet1Row++;
                    }
                }
                #endregion
                #region Border to All Cells           
                string sheet1modelRange = "A1:X" + (sheet1Row - 1).ToString();
                var sheet1modelTable = workSheet.Cells[sheet1modelRange];
                // Assign borders
                sheet1modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                #endregion
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
            return file.DownloadUrl;
        }
    }
}
