using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record MailRecipientState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public string RecipientEmail { get; init; } = "";
	public int SequenceNumber { get; init; }
	
	public ReportState? Report { get; init; }
	
	
}
