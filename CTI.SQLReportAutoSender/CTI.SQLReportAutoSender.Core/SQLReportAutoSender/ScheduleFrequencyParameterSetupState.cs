using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ScheduleFrequencyParameterSetupState : BaseEntity
{
	public string ScheduleFrequencyId { get; init; } = "";
	public string ScheduleParameterId { get; init; } = "";
	
	public ScheduleFrequencyState? ScheduleFrequency { get; init; }
	public ScheduleParameterState? ScheduleParameter { get; init; }
	
	
}
