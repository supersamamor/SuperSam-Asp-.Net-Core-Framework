using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record RentalTypeState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<TenantState>? TenantList { get; set; }
	
}
