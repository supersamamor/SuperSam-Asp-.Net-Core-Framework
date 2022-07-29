using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantContactState : BaseEntity
{
	public int Group { get; init; }
	public int Type { get; init; }
	public string? Detail { get; init; }
	public string TenantId { get; init; } = "";
	
	public TenantState? Tenant { get; init; }
	
	
}
