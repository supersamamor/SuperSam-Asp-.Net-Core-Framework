using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectDeliverableHistoryState : BaseEntity
{
	public string ProjectHistoryId { get; init; } = "";
	public string DeliverableId { get; init; } = "";
	public decimal? Amount { get; init; }
	public int? Sequence { get; init; }
	
	public ProjectHistoryState? ProjectHistory { get; init; }
	public DeliverableState? Deliverable { get; init; }
	
	
}
