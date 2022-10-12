using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ScheduleParameterState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<ScheduleFrequencyParameterSetupState>? ScheduleFrequencyParameterSetupList { get; set; }
	public IList<ReportScheduleSettingState>? ReportScheduleSettingList { get; set; }
	
}
public static class ScheduleParameter
{
	public static string Time { get; set; } = "Time";
	public static string Dayname { get; set; } = "Day name";
	public static string Daynumber { get; set; } = "Day number";
}