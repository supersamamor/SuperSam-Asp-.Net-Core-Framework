using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ClientFeedbackState : BaseEntity
{
	public string ClientFeedbackName { get; init; } = "";
	
	
	public IList<LeadTaskClientFeedBackState>? LeadTaskClientFeedBackList { get; set; }
	public IList<LeadTaskNextStepState>? LeadTaskNextStepList { get; set; }
	public IList<ActivityState>? ActivityList { get; set; }
	public IList<ActivityHistoryState>? ActivityHistoryList { get; set; }
	
}
