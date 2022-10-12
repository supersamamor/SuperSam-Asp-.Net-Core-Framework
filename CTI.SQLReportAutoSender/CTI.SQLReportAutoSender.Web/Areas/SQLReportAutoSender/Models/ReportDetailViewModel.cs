using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ReportDetailViewModel : BaseViewModel
{	
	[Display(Name = "Report Id")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Report Detail Number")]
	[Required]
	public int ReportDetailNumber { get; init; }
	[Display(Name = "Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	[Display(Name = "Connection String")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ConnectionString { get; init; } = "";
	[Display(Name = "Query String")]
	[Required]
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryString { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
