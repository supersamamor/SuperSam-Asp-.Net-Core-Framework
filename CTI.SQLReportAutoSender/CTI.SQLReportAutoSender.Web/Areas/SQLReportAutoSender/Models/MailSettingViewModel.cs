using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record MailSettingViewModel : BaseViewModel
{	
	[Display(Name = "Report Id")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Account")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	[EmailAddress]
	public string? Account { get; init; } = "";
	[Display(Name = "Password")]	
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Password { get; init; } = "";
	[Display(Name = "Body")]
	[Required]
	[StringLength(2500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Body { get; init; } = "";
	[Display(Name = "Subject")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Subject { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
