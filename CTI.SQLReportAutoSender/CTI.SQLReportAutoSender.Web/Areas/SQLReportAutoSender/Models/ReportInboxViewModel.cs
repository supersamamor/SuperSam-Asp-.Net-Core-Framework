using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ReportInboxViewModel : BaseViewModel
{	
	[Display(Name = "Report Id")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Status")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	[Display(Name = "Date/Time Sent")]
	public DateTime? DateTimeSent { get; init; } = DateTime.Now.Date;
	[Display(Name = "Remarks")]	
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
	public DateTime ReportDateTime { get; init; }


}
