using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

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
