using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ReportInboxState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public string Status { get; set; } = "";
	public DateTime? DateTimeSent { get; private set; }
	public DateTime ReportDateTime { get; init; }
	public string Remarks { get; private set; } = "";
	
	public ReportState? Report { get; init; }
	public void TagAsSent()
	{
		this.Status = ReportStatus.Sent;
		this.DateTimeSent = DateTime.Now;
	}
	public void TagAsFailed(string error)
	{
		this.Status = ReportStatus.Failed;
		this.DateTimeSent = DateTime.Now;
		this.Remarks = error;
	}
}
public static class ReportStatus
{
	public const string Pending = "Pending";
	public const string Sent = "Sent";
	public const string Failed = "Failed";
}
