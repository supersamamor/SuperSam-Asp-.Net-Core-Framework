using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantLotState : BaseEntity
{
	public decimal Area { get; init; }
	public string? LotNo { get; init; }
	public string TenantId { get; init; } = "";
	
	public TenantState? Tenant { get; init; }
	
	
}
