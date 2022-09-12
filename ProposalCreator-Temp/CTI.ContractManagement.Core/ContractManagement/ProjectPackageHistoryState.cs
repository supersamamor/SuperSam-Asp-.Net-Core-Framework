using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectPackageHistoryState : BaseEntity
{
	public string ProjectHistoryId { get; init; } = "";
	public int? OptionNumber { get; init; }
	public decimal? Amount { get; init; }
	
	public ProjectHistoryState? ProjectHistory { get; init; }
	
	public IList<ProjectPackageAdditionalDeliverableHistoryState>? ProjectPackageAdditionalDeliverableHistoryList { get; set; }
	
}
