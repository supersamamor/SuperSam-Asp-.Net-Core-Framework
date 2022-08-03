using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record RevalidateState : BaseEntity
{
	public DateTime SalesDate { get; init; }
	public string ProjectId { get; init; } = "";
	public string? TenantId { get; init; }
	public string Status { get; init; } = "";
	public string? ProcessingRemarks { get; init; }
	
	public ProjectState? Project { get; init; }
	public TenantState? Tenant { get; init; }
	
	
}
