using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportColumnHeaderViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	
	public string? ReportId { get; init; }
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Alias")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Alias { get; init; }
	[Display(Name = "Aggregation Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AggregationOperator { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	public IList<ReportColumnDetailViewModel>? ReportColumnDetailList { get; set; }
	
}
