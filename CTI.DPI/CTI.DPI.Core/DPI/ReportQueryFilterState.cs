using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

public record ReportQueryFilterState : BaseEntity
{
	public string? ReportId { get; init; }
	public string? FieldName { get; init; }
	public string? ComparisonOperator { get; init; }
	
	public ReportState? Report { get; init; }
	
	
}
