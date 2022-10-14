using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ReportInboxState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public string Status { get; init; } = "";
	public DateTime? DateTimeSent { get; init; }
	public DateTime ReportDateTime { get; init; }
	public string Remarks { get; init; } = "";
	
	public ReportState? Report { get; init; }
	
	
}
public static class ReportStatus
{
	public const string Pending = "Pending";
	public const string Sent = "Sent";
	public const string Failed = "Failed";
}