using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record TenantState : BaseEntity
{
	public string ProjectId { get; init; } = "";
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	public DateTime DateFrom { get; init; }
	public DateTime DateTo { get; init; }
	public bool IsDisabled { get; init; }
	
	public ProjectState? Project { get; init; }
	
	
}
