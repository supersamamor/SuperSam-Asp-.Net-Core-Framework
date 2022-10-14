using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record MailSettingState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public string? Account { get; init; } = "";
	public string? Password { get; init; } = "";
	public string Body { get; init; } = "";
	public string Subject { get; init; } = "";	
	public ReportState? Report { get; init; }
	
	
}
