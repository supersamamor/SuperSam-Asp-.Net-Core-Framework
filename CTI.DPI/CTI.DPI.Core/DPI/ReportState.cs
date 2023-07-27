using CTI.Common.Core.Base.Models;
namespace CTI.DPI.Core.DPI;
public record ReportState : BaseEntity
{
	public string ReportName { get; init; } = "";
	public string QueryType { get; init; } = "";
	public string ReportOrChartType { get; init; } = "";
	public bool IsDistinct { get; init; }
	public string? QueryString { get; init; }
    public bool DisplayOnDashboard { get; init; }
    public IList<ReportTableState>? ReportTableList { get; set; }
	public IList<ReportTableJoinParameterState>? ReportTableJoinParameterList { get; set; }
	public IList<ReportColumnHeaderState>? ReportColumnHeaderList { get; set; }
	public IList<ReportFilterGroupingState>? ReportFilterGroupingList { get; set; }
	public IList<ReportQueryFilterState>? ReportQueryFilterList { get; set; }
}
