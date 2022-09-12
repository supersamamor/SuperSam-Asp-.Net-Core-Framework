using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record DeliverableState : BaseEntity
{
	public string ProjectCategoryId { get; init; } = "";
	public string Name { get; init; } = "";
	
	public ProjectCategoryState? ProjectCategory { get; init; }
	
	public IList<ProjectDeliverableState>? ProjectDeliverableList { get; set; }
	public IList<ProjectPackageAdditionalDeliverableState>? ProjectPackageAdditionalDeliverableList { get; set; }
	public IList<ProjectDeliverableHistoryState>? ProjectDeliverableHistoryList { get; set; }
	public IList<ProjectPackageAdditionalDeliverableHistoryState>? ProjectPackageAdditionalDeliverableHistoryList { get; set; }
	
}
