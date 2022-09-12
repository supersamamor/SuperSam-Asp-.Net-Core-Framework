using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record PricingTypeState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<ProjectState>? ProjectList { get; set; }
	public IList<ProjectHistoryState>? ProjectHistoryList { get; set; }
	
}
