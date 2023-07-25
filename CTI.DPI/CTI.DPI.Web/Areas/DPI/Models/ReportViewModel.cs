using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportViewModel : BaseViewModel
{	
	[Display(Name = "Report Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportName { get; init; } = "";
	[Display(Name = "Query Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryType { get; init; } = "";
	[Display(Name = "Report / Chart Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportOrChartType { get; init; } = "";
	[Display(Name = "Distinct")]
	[Required]
	public bool IsDistinct { get; init; }
	[Display(Name = "Query String")]
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? QueryString { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<ReportTableViewModel>? ReportTableList { get; set; }
	public IList<ReportTableJoinParameterViewModel>? ReportTableJoinParameterList { get; set; }
	public IList<ReportColumnHeaderViewModel>? ReportColumnHeaderList { get; set; }
	public IList<ReportFilterGroupingViewModel>? ReportFilterGroupingList { get; set; }
	public IList<ReportQueryFilterViewModel>? ReportQueryFilterList { get; set; }
	
}
