using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record CustomScheduleState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public int SequenceNumber { get; init; }
	public DateTime DateTimeSchedule { get; init; }
	
	public ReportState? Report { get; init; }
	
	
}
