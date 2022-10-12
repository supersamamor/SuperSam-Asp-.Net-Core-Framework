using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ScheduleParameterViewModel : BaseViewModel
{	
	[Display(Name = "Description")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<ScheduleFrequencyParameterSetupViewModel>? ScheduleFrequencyParameterSetupList { get; set; }
	public IList<ReportScheduleSettingViewModel>? ReportScheduleSettingList { get; set; }
	
}
