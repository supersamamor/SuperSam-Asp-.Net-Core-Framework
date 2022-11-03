using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record NextStepState : BaseEntity
{
	public string NextStepTaskName { get; init; } = "";
	
	
	public IList<LeadTaskNextStepState>? LeadTaskNextStepList { get; set; }
	public IList<ActivityState>? ActivityList { get; set; }
	public IList<ActivityHistoryState>? ActivityHistoryList { get; set; }
	
}
