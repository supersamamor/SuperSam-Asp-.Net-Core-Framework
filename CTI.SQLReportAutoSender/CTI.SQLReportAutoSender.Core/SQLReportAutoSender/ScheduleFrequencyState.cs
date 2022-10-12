using CTI.Common.Core.Base.Models;
namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
public record ScheduleFrequencyState : BaseEntity
{
	public string Description { get; init; } = "";
	public IList<ScheduleFrequencyParameterSetupState>? ScheduleFrequencyParameterSetupList { get; set; }
	public IList<ReportState>? ReportList { get; set; }
	public IList<ReportScheduleSettingState>? ReportScheduleSettingList { get; set; }	
}
public static class Frequency
{
	public static string Monthly { get; set; } = "Monthly";
	public static string Weekly { get; set; } = "Weekly";
	public static string Daily { get; set; } = "Daily";
	public static string CustomDates { get; set; } = "Custom Dates";
}