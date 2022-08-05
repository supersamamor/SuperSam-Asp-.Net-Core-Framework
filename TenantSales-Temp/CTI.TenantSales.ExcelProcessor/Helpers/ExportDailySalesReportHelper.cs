using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.ExcelProcessor.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CTI.TenantSales.ExcelProcessor.Helpers
{
    public static class ExportDailySalesReportHelper
    {
        public static string Export(string staticPath, string fullFilePath, DateTime dateFrom, DateTime dateTo, IList<TenantDailySales> tenantList)
        {
            var fileName = "DailySalesReport-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            using (var package = new ExcelPackage(new FileInfo(Path.Combine(fullFilePath, fileName))))
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
                foreach (var tenantItem in tenantList)
                {
                    foreach (var posItem in tenantItem.TenantPOSList)
                    {
                        if (posItem.ShowSales)
                        {
                            foreach (var item in posItem.TenantPOSSalesList.OrderBy(l=>l.SalesDate).ToList())
                            {
                                item.SetDayNumber(dateFrom);
                                workSheet.Cells[sheet1Row, 1].Value = tenantItem.ProjectName;
                                workSheet.Cells[sheet1Row, 2].Value = tenantItem.TenantCode;
                                workSheet.Cells[sheet1Row, 3].Value = tenantItem.TenantName;
                                workSheet.Cells[sheet1Row, 4].Value = posItem.Code;
                                workSheet.Cells[sheet1Row, 5].Value = item.DayNumber;
                                workSheet.Cells[sheet1Row, 6].Value = item.SalesDate;
                                workSheet.Cells[sheet1Row, 7].Value = item.SalesCategory;
                                workSheet.Cells[sheet1Row, 8].Value = item.OldAccumulatedTotal;
                                workSheet.Cells[sheet1Row, 9].Value = item.NewAccumulatedTotal;
                                workSheet.Cells[sheet1Row, 10].Value = item.TaxableSalesAmount;
                                workSheet.Cells[sheet1Row, 11].Value = item.NonTaxableSalesAmount;
                                workSheet.Cells[sheet1Row, 12].Value = item.TotalServiceCharge;
                                workSheet.Cells[sheet1Row, 13].Value = item.TotalTax;
                                workSheet.Cells[sheet1Row, 14].Value = item.TotalNetSales;
                                workSheet.Cells[sheet1Row, 15].Value = item.RefundDiscount;
                                workSheet.Cells[sheet1Row, 16].Value = item.AdjustmentAmount;
                                workSheet.Cells[sheet1Row, 17].Value = item.VoidAmount;
                                workSheet.Cells[sheet1Row, 18].Value = item.SeniorCitizenDiscount;
                                workSheet.Cells[sheet1Row, 19].Value = item.PromoDiscount;
                                workSheet.Cells[sheet1Row, 20].Value = item.OtherDiscount;
                                workSheet.Cells[sheet1Row, 21].Value = item.NoOfTransactions;
                                workSheet.Cells[sheet1Row, 22].Value = item.NoOfSalesTransactions;
                                workSheet.Cells[sheet1Row, 23].Value = item.ValidationStatus == Convert.ToInt32(Core.Constants.ValidationStatusEnum.Passed) ? "Passed" : "Failed";
                                workSheet.Cells[sheet1Row, 24].Value = item.ValidationRemarks;
                                sheet1Row++;
                            }
                        }
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
            return staticPath + "\\" + fileName;
        }    
    }
}
