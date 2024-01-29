using CompanyPL.Common.Core.Base.Models;
namespace CompanyPL.ProjectPL.Core.ProjectPL;
public record ReportState : BaseEntity
{
	public string ReportName { get; init; } = "";
	public string QueryType { get; init; } = "";
	public string ReportOrChartType { get; init; } = "";
	public bool IsDistinct { get; init; }
	public string? QueryString { get; init; }
    public bool DisplayOnDashboard { get; init; }
	public bool DisplayOnReportModule { get; init; }
    public int Sequence { get; init; }   
	public IList<ReportQueryFilterState>? ReportQueryFilterList { get; set; }
    public IList<ReportRoleAssignmentState>? ReportRoleAssignmentList { get; set; }
}
public record ReportQueryFilterState : BaseEntity
{
    public string ReportId { get; init; } = "";
    public string FieldName { get; init; } = "";
    public string? FieldDescription { get; init; }
    public string DataType { get; init; } = "";
    public string? CustomDropdownValues { get; init; }
    public string? DropdownTableKeyAndValue { get; init; }
    public string? DropdownFilter { get; init; }
    public int Sequence { get; init; }
    public ReportState? Report { get; init; }
}
public record ReportRoleAssignmentState : BaseEntity
{
    public new string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReportId { get; init; } = "";
    public string RoleName { get; init; } = "";
    public ReportState? Report { get; init; }
}

