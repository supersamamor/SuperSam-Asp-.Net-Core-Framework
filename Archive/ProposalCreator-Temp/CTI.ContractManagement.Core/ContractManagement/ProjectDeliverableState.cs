using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectDeliverableState : BaseEntity
{
	public string ProjectId { get; init; } = "";
	public string DeliverableId { get; init; } = "";
	public decimal? Amount { get; init; }
	public int? Sequence { get; init; }
	
	public ProjectState? Project { get; init; }
	public DeliverableState? Deliverable { get; init; }
	
	
}
