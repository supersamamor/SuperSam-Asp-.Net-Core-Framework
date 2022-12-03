using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record ProjectState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	public string? ProjectAddress { get; init; }
	public string? Location { get; init; }
	public decimal LandArea { get; init; }
	public decimal GLA { get; init; }
	public bool IsDisabled { get; init; }
	
	public CompanyState? Company { get; init; }
	
	public IList<TenantState>? TenantList { get; set; }
	
}
