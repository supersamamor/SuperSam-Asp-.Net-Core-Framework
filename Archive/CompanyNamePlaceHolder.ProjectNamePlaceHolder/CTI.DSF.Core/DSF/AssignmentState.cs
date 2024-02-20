using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record AssignmentState : BaseEntity
{
	public string AssignmentCode { get; init; } = "";
	public string TaskCompanyAssignmentId { get; init; } = "";
	public string PrimaryAssignee { get; init; } = "";
	public string? AlternateAssignee { get; init; }
	public DateTime StartDate { get; init; }
	public DateTime EndDate { get; init; }
	
	public TaskCompanyAssignmentState? TaskCompanyAssignment { get; init; }
	
	public IList<DeliveryState>? DeliveryList { get; set; }
	
}
