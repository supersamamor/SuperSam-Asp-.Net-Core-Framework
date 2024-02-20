using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectMilestoneState : BaseEntity
{
	public string ProjectId { get; init; } = "";
	public string MilestoneStageId { get; init; } = "";
	public int? Sequence { get; init; }
	public string FrequencyId { get; init; } = "";
	public int? FrequencyQuantity { get; init; }
	
	public ProjectState? Project { get; init; }
	public MilestoneStageState? MilestoneStage { get; init; }
	public FrequencyState? Frequency { get; init; }
	
	
}
