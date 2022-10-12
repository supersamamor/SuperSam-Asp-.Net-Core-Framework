using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ReportScheduleSettingState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public string ScheduleFrequencyId { get; init; } = "";
	public string ScheduleParameterId { get; init; } = "";
	public string Value { get; init; } = "";
	
	public ReportState? Report { get; init; }
	public ScheduleFrequencyState? ScheduleFrequency { get; init; }
	public ScheduleParameterState? ScheduleParameter { get; init; }
	
	
}
