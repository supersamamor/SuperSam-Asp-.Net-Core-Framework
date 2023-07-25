using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportColumnDetailViewModel : BaseViewModel
{	
	[Display(Name = "Report Column")]
	
	public string? ReportColumnId { get; init; }
	public string?  ForeignKeyReportColumnHeader { get; set; }
	[Display(Name = "Table")]
	
	public string? TableId { get; init; }
	public string?  ForeignKeyReportTable { get; set; }
	[Display(Name = "Field Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get; init; }
	[Display(Name = "Function")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Function { get; init; }
	[Display(Name = "Arithmetic Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ArithmeticOperator { get; init; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportColumnHeaderViewModel? ReportColumnHeader { get; init; }
	public ReportTableViewModel? ReportTable { get; init; }
		
	
}
