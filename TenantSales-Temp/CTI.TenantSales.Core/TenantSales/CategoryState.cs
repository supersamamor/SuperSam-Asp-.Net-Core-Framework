using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record CategoryState : BaseEntity
{
	public string Name { get; init; } = "";
	public string ClassificationId { get; init; } = "";
	public string Code { get; init; } = "";	
	public ClassificationState? Classification { get; init; }
	public IList<TenantState>? TenantList { get; init; }
}
