using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ClientState : BaseEntity
{
	public string ContactPersonName { get; init; } = "";
	public string ContactPersonPosition { get; init; } = "";
	public string? CompanyName { get; init; }
	public string? CompanyDescription { get; init; }
	public string CompanyAddressLineOne { get; init; } = "";
	public string? CompanyAddressLineTwo { get; init; }
	public string EmailAddress { get; init; } = "";
	
	
	public IList<ProjectState>? ProjectList { get; set; }
	public IList<ProjectHistoryState>? ProjectHistoryList { get; set; }
	
}
