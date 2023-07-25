using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportFilterGroupingViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	
	public string? ReportId { get; init; }
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Logical Operator")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get; init; }
	[Display(Name = "Level")]
	public int? GroupLevel { get; init; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	public IList<ReportColumnFilterViewModel>? ReportColumnFilterList { get; set; }
	
}
