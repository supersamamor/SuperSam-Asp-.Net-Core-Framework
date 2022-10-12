using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ReportScheduleSettingViewModel : BaseViewModel
{	
	[Display(Name = "ReportId")]
	[Required]
	
	public string ReportId { get; init; } = "";
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "ScheduleFrequencyId")]
	[Required]
	
	public string ScheduleFrequencyId { get; init; } = "";
	public string?  ForeignKeyScheduleFrequency { get; set; }
	[Display(Name = "ScheduleParameterId")]
	[Required]
	
	public string ScheduleParameterId { get; init; } = "";
	public string?  ForeignKeyScheduleParameter { get; set; }
	[Display(Name = "Value")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Value { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
	public ScheduleFrequencyViewModel? ScheduleFrequency { get; init; }
	public ScheduleParameterViewModel? ScheduleParameter { get; init; }
		
	
}
