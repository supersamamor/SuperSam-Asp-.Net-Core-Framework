using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportQueryFilterViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	
	public string? ReportId { get; init; }
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Field Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get; init; }
	[Display(Name = "Comparison Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ComparisonOperator { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
