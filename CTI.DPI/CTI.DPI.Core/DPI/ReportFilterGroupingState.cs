using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

public record ReportFilterGroupingState : BaseEntity
{
	public string? ReportId { get; init; }
	public string? LogicalOperator { get; init; }
	public int? GroupLevel { get; init; }
	public int? Sequence { get; init; }
	
	public ReportState? Report { get; init; }
	
	public IList<ReportColumnFilterState>? ReportColumnFilterList { get; set; }
	
}
