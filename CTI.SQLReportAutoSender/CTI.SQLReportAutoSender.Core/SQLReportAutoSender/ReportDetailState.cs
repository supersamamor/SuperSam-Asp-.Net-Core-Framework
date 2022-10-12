using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ReportDetailState : BaseEntity
{
	public string ReportId { get; init; } = "";
	public int ReportDetailNumber { get; init; }
	public string Description { get; init; } = "";
	public string ConnectionString { get; init; } = "";
	public string QueryString { get; init; } = "";
	
	public ReportState? Report { get; init; }
	
	
}
