using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record LevelState : BaseEntity
{
	public string Name { get; init; } = "";
	public string ProjectId { get; init; } = "";
	public bool HasPercentageSalesTenant { get; init; }
	public bool IsDisabled { get; init; }
	
	public ProjectState? Project { get; init; }
	
	public IList<TenantState>? TenantList { get; set; }
	
}
