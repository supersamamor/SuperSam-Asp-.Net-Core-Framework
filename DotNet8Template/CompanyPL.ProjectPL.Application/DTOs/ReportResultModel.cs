
namespace CompanyPL.ProjectPL.Application.DTOs
{
    public class ReportResultModel
    {
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? Results { get; set; }
        public string? ColumnHeaders { get; set; }
        public string ReportOrChartType { get; set; } = "";
        public bool DisplayLegend { get; set; }
        public IList<ReportQueryFilterModel> Filters { get; set; } = new List<ReportQueryFilterModel>();
    }
    public class ReportQueryFilterModel
    {
        public string FieldName { get; set; } = "";
        public string FieldDescription { get; set; } = "";
        public string DataType { get; set; } = "";
        public string FieldValue { get; set; } = "";
        public string ReportId { get; init; } = "";
        public string? CustomDropdownValues { get; init; }
        public string? DropdownTableKeyAndValue { get; init; }
        public string? DropdownFilter { get; init; }
        public int Sequence { get; init; }
    }
}
