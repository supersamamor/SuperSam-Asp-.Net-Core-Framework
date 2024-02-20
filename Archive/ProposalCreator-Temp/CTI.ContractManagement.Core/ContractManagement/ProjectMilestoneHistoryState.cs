using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectMilestoneHistoryState : BaseEntity
{
	public string ProjectHistoryId { get; init; } = "";
	public string MilestoneStageId { get; init; } = "";
	public int? Sequence { get; init; }
	public string FrequencyId { get; init; } = "";
	public int? FrequencyQuantity { get; init; }
	
	public ProjectHistoryState? ProjectHistory { get; init; }
	public MilestoneStageState? MilestoneStage { get; init; }
	public FrequencyState? Frequency { get; init; }
	
	
}
