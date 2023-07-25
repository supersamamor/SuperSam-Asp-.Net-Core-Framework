using CTI.Common.Web.Utility.Extensions;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportTableViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Table Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TableName { get; init; } = "";
	[Display(Name = "Alias")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Alias { get; init; }
	[Display(Name = "Join Type")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? JoinType { get; init; }
	[Display(Name = "Sequence")]
	[Required]
	public int Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	public IList<ReportTableJoinParameterViewModel>? ReportTableJoinParameterList { get; set; }
	public IList<ReportColumnDetailViewModel>? ReportColumnDetailList { get; set; }
	public IList<ReportColumnFilterViewModel>? ReportColumnFilterList { get; set; }
	
}
