using CTI.Common.Core.Base.Models;

namespace CTI.ELMS.Core.ELMS;

public record ActivityState : BaseEntity
{
    public string? LeadID { get; init; }
    public string? ProjectID { get; init; }
    public string? LeadTaskId { get; init; }
    public DateTime? ActivityDate { get; init; }
    public string? ClientFeedbackId { get; init; }
    public string? NextStepId { get; init; }
    public DateTime? TargetDate { get; init; }
    public string? ActivityRemarks { get; init; }
    public DateTime? PCTDate { get; private set; }
    public LeadState? Lead { get; init; }
    public ProjectState? Project { get; init; }
    public LeadTaskState? LeadTask { get; init; }
    public ClientFeedbackState? ClientFeedback { get; init; }
    public NextStepState? NextStep { get; init; }
    public IList<ActivityHistoryState>? ActivityHistoryList { get; set; }
    public IList<UnitActivityState>? UnitActivityList { get; set; }
    public string UnitInformation
    {
        get
        {
            var unitList = this.UnitActivityList?.Select(l => l.Unit?.UnitNo).ToList();
            if (unitList != null)
            {
                return string.Join(", ", unitList);
            }
            else
            {
                return "";
            }
        }
    }
    public void SetPCTDate(DateTime? pctDate)
    {
        this.PCTDate = pctDate;
    }
}
