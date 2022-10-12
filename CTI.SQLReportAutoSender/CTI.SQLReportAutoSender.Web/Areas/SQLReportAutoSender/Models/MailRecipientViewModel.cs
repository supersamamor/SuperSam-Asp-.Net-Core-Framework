using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record MailRecipientViewModel : BaseViewModel
{	
	[Display(Name = "Report Id")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Recipient Email")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	[EmailAddress]
	public string RecipientEmail { get; init; } = "";
	[Display(Name = "Sequence Number")]
	[Required]
	public int SequenceNumber { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
