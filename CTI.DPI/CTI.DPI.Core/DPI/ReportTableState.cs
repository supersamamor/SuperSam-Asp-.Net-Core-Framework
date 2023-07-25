using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

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
