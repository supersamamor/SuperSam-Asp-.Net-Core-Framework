using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectPackageAdditionalDeliverableHistoryState : BaseEntity
{
	public string ProjectPackageHistoryId { get; init; } = "";
	public string DeliverableId { get; init; } = "";
	public int? Sequence { get; init; }
	
	public ProjectPackageHistoryState? ProjectPackageHistory { get; init; }
	public DeliverableState? Deliverable { get; init; }
	
	
}
