using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record SalesCategoryState : BaseEntity
{
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	public decimal Rate { get; init; }
	public string TenantId { get; init; } = "";
	public bool IsDisabled { get; init; }
	
	public TenantState? Tenant { get; init; }
	
	
}
