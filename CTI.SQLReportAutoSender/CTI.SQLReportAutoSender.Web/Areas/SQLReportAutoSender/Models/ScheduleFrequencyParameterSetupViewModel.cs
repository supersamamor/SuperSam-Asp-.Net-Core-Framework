using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ScheduleFrequencyParameterSetupViewModel : BaseViewModel
{	
	[Display(Name = "ScheduleFrequencyId")]
	[Required]
	
	public string ScheduleFrequencyId { get; init; } = "";
	public string?  ForeignKeyScheduleFrequency { get; set; }
	[Display(Name = "ScheduleParameterId")]
	[Required]
	
	public string ScheduleParameterId { get; init; } = "";
	public string?  ForeignKeyScheduleParameter { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public ScheduleFrequencyViewModel? ScheduleFrequency { get; init; }
	public ScheduleParameterViewModel? ScheduleParameter { get; init; }
		
	
}
