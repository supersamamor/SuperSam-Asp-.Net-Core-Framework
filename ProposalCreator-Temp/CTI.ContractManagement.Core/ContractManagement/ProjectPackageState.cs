using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectPackageState : BaseEntity
{
	public string ProjectId { get; init; } = "";
	public int? OptionNumber { get; init; }
	public decimal? Amount { get; init; }
	
	public ProjectState? Project { get; init; }
	
	public IList<ProjectPackageAdditionalDeliverableState>? ProjectPackageAdditionalDeliverableList { get; set; }
	
}
