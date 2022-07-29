using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record BusinessUnitState : BaseEntity
{
	public string Name { get; init; } = "";
	public bool IsDisabled { get; init; }
	
	
	public IList<ProjectBusinessUnitState>? ProjectBusinessUnitList { get; set; }
	
}
