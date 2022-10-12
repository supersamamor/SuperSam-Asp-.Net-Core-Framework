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
	[Display(Name = "ScheduleFrequencyId")]
	[Required]
	
	public string ScheduleFrequencyId { get; init; } = "";
	public string?  ForeignKeyScheduleFrequency { get; set; }
	[Display(Name = "IsActive")]
	[Required]
	public bool IsActive { get; init; }
	[Display(Name = "LatestFileGeneratedPath")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LatestFileGeneratedPath { get; init; } = "";
	[Display(Name = "MultipleReportType")]
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
