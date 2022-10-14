using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace CTI.SQLReportAutoSender.ReportGenerator.Helper
{
    public class ExcelReportGeneratorService
    {
        private readonly string _uploadFilePath;
        public ExcelReportGeneratorService(IConfiguration configuration)
        {
            _uploadFilePath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        }
        public IList<string> GenerateReport(ReportInboxState reportInbox)
        {
            IList<string> files = new List<string>();
            if (reportInbox.Report!.MultipleReportType == MultipleReportTypeItems.MultipleSheets)
            {
                files = GenerateReportMultipleSheet(reportInbox);
            }
            else if (reportInbox.Report!.MultipleReportType == MultipleReportTypeItems.MultipleFile)
            {
                files = GenerateReportMultipleFiles(reportInbox);
            }
            return files;
        }
        private IList<string> GenerateReportMultipleSheet(ReportInboxState reportInbox)
        {
            IList<string> files = new List<string>();
            string fileName = reportInbox!.Report!.Description.Replace(" ", "-") + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xlsx";
            string filePath = Path.Combine(_uploadFilePath, fileName);
            files.Add(filePath);
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                int reportCounter = 1;
                foreach (var reportDetail in reportInbox.Report!.ReportDetailList!)
                {
                    var workSheet1 = package.Workbook.Worksheets.Add("Report No. " + reportCounter);
                    using (SqlConnection connection = new(reportDetail.ConnectionString))
                    {
                        string query = reportDetail.QueryString;
                        using SqlCommand command = new(query, connection);
                        using SqlDataAdapter dataAdapter = new(command);
                        using DataSet dataSet = new();
                        command.CommandTimeout = 0;
                        dataAdapter.Fill(dataSet);
                        int headerColumnCounter = 0;
                        //Generate Header
                        int workSheetRow = 1;
                        int columnCount = dataSet.Tables[0].Columns.Count;
                        foreach (var column in dataSet.Tables[0].Columns)
                        {
                            workSheet1.Cells[workSheetRow, headerColumnCounter + 1].Value = dataSet.Tables[0].Columns[headerColumnCounter].ColumnName;
                            headerColumnCounter++;
                        }
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Font.Bold = true;
                        workSheetRow++;
                        //Generate Data
                        int dataRowCounter = 0;
                        foreach (var rows in dataSet.Tables[0].Rows)
                        {
                            int dataColumnCounter = 0;
                            foreach (var column in dataSet.Tables[0].Columns)
                            {
                                workSheet1.Cells[workSheetRow, 1].Value = dataSet.Tables[0]?.Rows[dataRowCounter][dataColumnCounter]?.ToString();
                                dataColumnCounter++;
                            }
                            dataRowCounter++;
                            workSheetRow++;
                        }
                        connection.Close();
                        #region Border to All Cells
                        var sheet2modelTable = workSheet1.Cells[1, 1, workSheetRow, columnCount];
                        //Assign borders
                        sheet2modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        #endregion
                        workSheet1.Cells[workSheet1.Dimension.Address].AutoFitColumns();                       
                    }
                    reportCounter++;
                }
                package.Save();
            }
            return files;
        }
        private IList<string> GenerateReportMultipleFiles(ReportInboxState reportInbox)
        {
            IList<string> files = new List<string>();
            int reportCounter = 1;
            foreach (var reportDetail in reportInbox.Report!.ReportDetailList!)
            {
                string fileName = reportInbox!.Report!.Description.Replace(" ", "-") + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-ReportNo-" + reportCounter + ".xlsx";
                string filePath = Path.Combine(_uploadFilePath, fileName);
                files.Add(filePath);
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet1 = package.Workbook.Worksheets.Add("Report");
                    using (SqlConnection connection = new(reportDetail.ConnectionString))
                    {
                        string query = reportDetail.QueryString;
                        using SqlCommand command = new(query, connection);
                        using SqlDataAdapter dataAdapter = new(command);
                        using DataSet dataSet = new();
                        command.CommandTimeout = 0;
                        dataAdapter.Fill(dataSet);
                        int headerColumnCounter = 0;
                        //Generate Header
                        int workSheetRow = 1;
                        int columnCount = dataSet.Tables[0].Columns.Count;
                        foreach (var column in dataSet.Tables[0].Columns)
                        {
                            workSheet1.Cells[workSheetRow, headerColumnCounter + 1].Value = dataSet.Tables[0].Columns[headerColumnCounter].ColumnName;
                            headerColumnCounter++;
                        }
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        workSheet1.Cells[workSheetRow, 1, workSheetRow, headerColumnCounter].Style.Font.Bold = true;
                        workSheetRow++;
                        //Generate Data
                        int dataRowCounter = 0;
                        foreach (var rows in dataSet.Tables[0].Rows)
                        {
                            int dataColumnCounter = 0;
                            foreach (var column in dataSet.Tables[0].Columns)
                            {
                                workSheet1.Cells[workSheetRow, 1].Value = dataSet.Tables[0]?.Rows[dataRowCounter][dataColumnCounter]?.ToString();
                                dataColumnCounter++;
                            }
                            dataRowCounter++;
                            workSheetRow++;
                        }
                        connection.Close();
                        #region Border to All Cells
                        var sheet2modelTable = workSheet1.Cells[1, 1, workSheetRow, columnCount];
                        //Assign borders
                        sheet2modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet2modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        #endregion
                        workSheet1.Cells[workSheet1.Dimension.Address].AutoFitColumns();
                        package.Save();
                    }
                    reportCounter++;
                }
            }
            return files;
        }
    }
}
