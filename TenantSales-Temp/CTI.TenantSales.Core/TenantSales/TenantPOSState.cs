using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantPOSState : BaseEntity
{
	public string Code { get; init; } = "";
	public string TenantId { get; init; } = "";
	public string? SerialNumber { get; init; }
	public bool IsDisabled { get; init; }
	
	public TenantState? Tenant { get; init; }
	
	public IList<TenantPOSSalesState>? TenantPOSSalesList { get; set; }
	
}
