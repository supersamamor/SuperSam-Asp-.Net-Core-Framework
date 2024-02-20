using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectCategoryState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<DeliverableState>? DeliverableList { get; set; }
	
}
