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
    public int Sequence { get; init; }
    public IList<ReportTableState>? ReportTableList { get; set; }
	public IList<ReportTableJoinParameterState>? ReportTableJoinParameterList { get; set; }
	public IList<ReportColumnHeaderState>? ReportColumnHeaderList { get; set; }
	public IList<ReportFilterGroupingState>? ReportFilterGroupingList { get; set; }
	public IList<ReportQueryFilterState>? ReportQueryFilterList { get; set; }
    public IList<ReportRoleAssignmentState>? ReportRoleAssignmentList { get; set; }
}

public record ReportTableState : BaseEntity
{
    public string ReportId { get; init; } = "";
    public string TableName { get; init; } = "";
    public string? Alias { get; init; }
    public string? JoinType { get; init; }
    public int Sequence { get; init; }
    public ReportState? Report { get; init; }
    public IList<ReportTableJoinParameterState>? ReportTableJoinParameterList { get; set; }
    public IList<ReportColumnDetailState>? ReportColumnDetailList { get; set; }
    public IList<ReportColumnFilterState>? ReportColumnFilterList { get; set; }

}
public record ReportTableJoinParameterState : BaseEntity
{
    public string ReportId { get; init; } = "";
    public string? LogicalOperator { get; init; }
    public string TableId { get; init; } = "";
    public string FieldName { get; init; } = "";
    public string JoinFromTableId { get; init; } = "";
    public string JoinFromFieldName { get; init; } = "";
    public int Sequence { get; init; }
    public ReportState? Report { get; init; }
    public ReportTableState? ReportTable { get; init; }
}

public record ReportColumnHeaderState : BaseEntity
{
    public string? ReportId { get; init; }
    public string? Alias { get; init; }
    public string? AggregationOperator { get; init; }
    public ReportState? Report { get; init; }
    public IList<ReportColumnDetailState>? ReportColumnDetailList { get; set; }

}
public record ReportColumnDetailState : BaseEntity
{
    public string? ReportColumnId { get; init; }
    public string? TableId { get; init; }
    public string? FieldName { get; init; }
    public string? Function { get; init; }
    public string? ArithmeticOperator { get; init; }
    public int? Sequence { get; init; }
    public ReportColumnHeaderState? ReportColumnHeader { get; init; }
    public ReportTableState? ReportTable { get; init; }
}

public record ReportColumnFilterState : BaseEntity
{
    public string? ReportFilterGroupingId { get; init; }
    public string? LogicalOperator { get; init; }
    public string? TableId { get; init; }
    public string? FieldName { get; init; }
    public string? ComparisonOperator { get; init; }
    public bool IsString { get; init; }
    public ReportFilterGroupingState? ReportFilterGrouping { get; init; }
    public ReportTableState? ReportTable { get; init; }

}

public record ReportFilterGroupingState : BaseEntity
{
    public string? ReportId { get; init; }
    public string? LogicalOperator { get; init; }
    public int? GroupLevel { get; init; }
    public int? Sequence { get; init; }
    public ReportState? Report { get; init; }
    public IList<ReportColumnFilterState>? ReportColumnFilterList { get; set; }
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

