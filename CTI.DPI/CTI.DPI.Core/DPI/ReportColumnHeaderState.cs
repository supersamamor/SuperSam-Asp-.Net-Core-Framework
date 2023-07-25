using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

public record ReportColumnHeaderState : BaseEntity
{
	public string? ReportId { get; init; }
	public string? Alias { get; init; }
	public string? AggregationOperator { get; init; }
	
	public ReportState? Report { get; init; }
	
	public IList<ReportColumnDetailState>? ReportColumnDetailList { get; set; }
	
}
