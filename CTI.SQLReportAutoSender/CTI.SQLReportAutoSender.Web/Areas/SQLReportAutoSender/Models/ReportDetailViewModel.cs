using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ReportDetailViewModel : BaseViewModel
{	
	[Display(Name = "ReportId")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "ReportDetailNumber")]
	[Required]
	public int ReportDetailNumber { get; init; }
	[Display(Name = "Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	[Display(Name = "ConnectionString")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ConnectionString { get; init; } = "";
	[Display(Name = "QueryString")]
	[Required]
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryString { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
