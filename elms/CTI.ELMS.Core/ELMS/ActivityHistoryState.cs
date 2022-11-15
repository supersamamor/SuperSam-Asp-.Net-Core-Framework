using CTI.Common.Core.Base.Models;

namespace CTI.ELMS.Core.ELMS;

public record ActivityHistoryState : BaseEntity
{
	public string? ActivityID { get; init; }
	public string? LeadTaskId { get; init; }
	public DateTime? ActivityDate { get; init; }
	public string? ClientFeedbackId { get; init; }
	public string? NextStepId { get; init; }
	public DateTime? TargetDate { get; init; }
	public string? ActivityRemarks { get; init; }
	public DateTime? PCTDate { get; init; }
	public string? UnitsInformation
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
	public ActivityState? Activity { get; init; }
	public LeadTaskState? LeadTask { get; init; }
	public ClientFeedbackState? ClientFeedback { get; init; }
	public NextStepState? NextStep { get; init; }	
	public IList<UnitActivityState>? UnitActivityList { get; set; }	
}
