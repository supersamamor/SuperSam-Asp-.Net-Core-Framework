using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ContractManagement.Core.ContractManagement;

public record ApplicationConfigurationState : BaseEntity
{
	public string? Logo { get; init; } = "";
	public string AddressLineOne { get; init; } = "";
	public string? AddressLineTwo { get; init; }
	public string OrganizationOverview { get; init; } = "";
	public string DocumentFooter { get; init; } = "";
	
	
	
}
