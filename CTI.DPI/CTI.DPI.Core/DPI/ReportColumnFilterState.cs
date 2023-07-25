using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

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
