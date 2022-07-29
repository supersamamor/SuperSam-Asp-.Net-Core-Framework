using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record ProjectBusinessUnitState : BaseEntity
{
	public string BusinessUnitId { get; init; } = "";
	public string ProjectId { get; init; } = "";
	
	public BusinessUnitState? BusinessUnit { get; init; }
	public ProjectState? Project { get; init; }
	
	
}
