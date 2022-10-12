using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ReportViewModel : BaseViewModel
{	
	[Display(Name = "Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	[Display(Name = "Schedule Frequency")]
	[Required]
	
	public string ScheduleFrequencyId { get; init; } = "";
	public string?  ForeignKeyScheduleFrequency { get; set; }
	[Display(Name = "Active")]
	[Required]
	public bool IsActive { get; init; }
	[Display(Name = "Latest File Generated Path")]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LatestFileGeneratedPath { get; init; } = "";
	[Display(Name = "Multiple Report Type")]
	[Required]
	public int MultipleReportType { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ScheduleFrequencyViewModel? ScheduleFrequency { get; init; }
		
	public IList<ReportDetailViewModel>? ReportDetailList { get; set; }
	public IList<MailSettingViewModel>? MailSettingList { get; set; }
	public IList<MailRecipientViewModel>? MailRecipientList { get; set; }
	public IList<ReportScheduleSettingViewModel>? ReportScheduleSettingList { get; set; }
	public IList<CustomScheduleViewModel>? CustomScheduleList { get; set; }
	public IList<ReportInboxViewModel>? ReportInboxList { get; set; }
	
}
