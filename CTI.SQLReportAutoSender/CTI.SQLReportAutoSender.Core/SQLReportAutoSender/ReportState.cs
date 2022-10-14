using CTI.Common.Core.Base.Models;
namespace CTI.SQLReportAutoSender.Core.SQLReportAutoSender;

public record ReportState : BaseEntity
{
    public string Description { get; init; } = "";
    public string ScheduleFrequencyId { get; init; } = "";
    public bool IsActive { get; init; }
    public string? LatestFileGeneratedPath { get; init; } = "";
    public int MultipleReportType { get; init; }
    public ScheduleFrequencyState? ScheduleFrequency { get; init; }
    public IList<ReportDetailState>? ReportDetailList { get; set; }
    public IList<MailSettingState>? MailSettingList { get; set; }
    public IList<MailRecipientState>? MailRecipientList { get; set; }
    public IList<ReportScheduleSettingState>? ReportScheduleSettingList { get; set; }
    public IList<CustomScheduleState>? CustomScheduleList { get; set; }
    public IList<ReportInboxState>? ReportInboxList { get; set; }
}
public static class MultipleReportTypeItems
{
    public static int MultipleSheets { get; set; } = 1;
    public static int MultipleFile { get; set; } = 2;
}