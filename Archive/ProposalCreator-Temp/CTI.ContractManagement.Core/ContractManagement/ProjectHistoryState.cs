using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ProjectHistoryState : BaseEntity
{
	public string ProjectId { get; init; } = "";
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
	
	public ProjectState? Project { get; init; }
	public ClientState? Client { get; init; }
	public PricingTypeState? PricingType { get; init; }
	
	public IList<ProjectDeliverableHistoryState>? ProjectDeliverableHistoryList { get; set; }
	public IList<ProjectMilestoneHistoryState>? ProjectMilestoneHistoryList { get; set; }
	public IList<ProjectPackageHistoryState>? ProjectPackageHistoryList { get; set; }
	
}
