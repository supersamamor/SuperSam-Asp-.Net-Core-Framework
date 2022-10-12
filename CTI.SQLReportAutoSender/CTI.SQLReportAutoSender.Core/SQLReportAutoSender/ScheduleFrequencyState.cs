using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ScheduleFrequencyState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<ScheduleFrequencyParameterSetupState>? ScheduleFrequencyParameterSetupList { get; set; }
	public IList<ReportState>? ReportList { get; set; }
	public IList<ReportScheduleSettingState>? ReportScheduleSettingList { get; set; }
	
}
