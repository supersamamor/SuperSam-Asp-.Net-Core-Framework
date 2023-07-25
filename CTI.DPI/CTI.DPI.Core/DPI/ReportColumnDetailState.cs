using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DPI.Core.DPI;

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
