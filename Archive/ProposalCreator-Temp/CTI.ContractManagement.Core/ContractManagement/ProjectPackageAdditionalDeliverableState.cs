using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectPackageAdditionalDeliverableState : BaseEntity
{
	public string ProjectPackageId { get; init; } = "";
	public string DeliverableId { get; init; } = "";
	public int? Sequence { get; init; }
	
	public ProjectPackageState? ProjectPackage { get; init; }
	public DeliverableState? Deliverable { get; init; }
	
	
}
