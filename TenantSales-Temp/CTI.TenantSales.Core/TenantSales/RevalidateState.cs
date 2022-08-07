using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record RevalidateState : BaseEntity
{
	public DateTime SalesDate { get; init; }
	public string ProjectId { get; init; } = "";
	public string? TenantId { get; init; }
	public string Status { get; private set; } = TenantSales.Status.Pending;
	public string? ProcessingRemarks { get; private set; }	
	public ProjectState? Project { get; init; }
	public TenantState? Tenant { get; init; }	
	public void SetDone()
	{
		this.Status = TenantSales.Status.Done;
	}
	public void SetFailed(string remarks)
	{
		this.Status = TenantSales.Status.Failed;
		this.ProcessingRemarks = remarks;
	}
}
public static class Status
{
	public const string Done = "Done";
	public const string Failed = "Failed";
	public const string Pending = "Pending";
}