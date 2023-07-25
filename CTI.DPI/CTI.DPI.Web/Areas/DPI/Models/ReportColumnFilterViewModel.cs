using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportColumnFilterViewModel : BaseViewModel
{	
	[Display(Name = "Report Filter Grouping")]
	
	public string? ReportFilterGroupingId { get; init; }
	public string?  ForeignKeyReportFilterGrouping { get; set; }
	[Display(Name = "Logical Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get; init; }
	[Display(Name = "Table")]
	
	public string? TableId { get; init; }
	public string?  ForeignKeyReportTable { get; set; }
	[Display(Name = "Field Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get; init; }
	[Display(Name = "Comparison Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ComparisonOperator { get; init; }
	[Display(Name = "Is String")]
	public bool IsString { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportFilterGroupingViewModel? ReportFilterGrouping { get; init; }
	public ReportTableViewModel? ReportTable { get; init; }
		
	
}
