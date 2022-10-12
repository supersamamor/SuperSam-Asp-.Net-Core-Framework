using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record CustomScheduleViewModel : BaseViewModel
{	
	[Display(Name = "Report Id")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Sequence Number")]
	[Required]
	public int SequenceNumber { get; init; }
	[Display(Name = "Date/Time Schedule")]
	[Required]
	public DateTime DateTimeSchedule { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
		
	
}
