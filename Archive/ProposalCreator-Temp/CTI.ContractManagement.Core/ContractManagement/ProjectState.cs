using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectState : BaseEntity
{
	public string ClientId { get; init; } = "";
	public string ProjectName { get; init; } = "";
	public string ProjectDescription { get; init; } = "";
	public string? Logo { get; init; }
	public string ProjectGoals { get; init; } = "";
	public decimal? Discount { get; init; }
	public string PricingTypeId { get; init; } = "";
	public bool EnablePricing { get; init; }
	public string? Template { get; init; }
	public string RevisionSummary { get; init; } = "";
	public string DocumentCode { get; init; } = "";
	
	public ClientState? Client { get; init; }
	public PricingTypeState? PricingType { get; init; }
	
	public IList<ProjectDeliverableState>? ProjectDeliverableList { get; set; }
	public IList<ProjectMilestoneState>? ProjectMilestoneList { get; set; }
	public IList<ProjectPackageState>? ProjectPackageList { get; set; }
	public IList<ProjectHistoryState>? ProjectHistoryList { get; set; }
	
}
