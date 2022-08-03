namespace CTI.TenantSales.Web.Areas.Reports.Models
{
    public class DailySalesModel
    {
        public int ReportType { get; set; } = (int)ReportTypeEnum.Daily;
        public int Year { get; set; } = DateTime.Today.Year;
        public int Month { get; set; } = DateTime.Today.Month - 1;
        public DateTime? WeekStartDate { get; set; }
        public string? LevelId { get; set; }
        public string? TenantId { get; set; }
        public string? ProjectId { get; set; }
        public int OutputType { get; set; }
        public string? ExcelPath { get; set; }
    }
    public enum ReportTypeEnum
    {
        Daily = 1,
        Weekly = 2
    }
    public enum ReportOutputTypeEnum
    {
        PDF = 1,
        Excel = 2,
    }
}
