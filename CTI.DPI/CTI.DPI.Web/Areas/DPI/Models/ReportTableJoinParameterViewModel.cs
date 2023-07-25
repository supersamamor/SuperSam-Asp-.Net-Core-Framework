using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportTableJoinParameterViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Logical Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get; init; }
	[Display(Name = "Table")]
	[Required]
	
	public string TableId { get; init; } = "";
	public string?  ForeignKeyReportTable { get; set; }
	[Display(Name = "Field Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FieldName { get; init; } = "";
	[Display(Name = "Join from Table")]
	[Required]
	
	public string JoinFromTableId { get; init; } = "";
	[Display(Name = "Join from FieldName")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string JoinFromFieldName { get; init; } = "";
	[Display(Name = "Sequence")]
	[Required]
	public int Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
	public ReportTableViewModel? ReportTable { get; init; }
		
	
}
