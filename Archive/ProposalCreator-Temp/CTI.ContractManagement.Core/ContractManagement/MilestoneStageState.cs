using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record MilestoneStageState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<ProjectMilestoneState>? ProjectMilestoneList { get; set; }
	public IList<ProjectMilestoneHistoryState>? ProjectMilestoneHistoryList { get; set; }
	
}
