using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskApproverState : BaseEntity
{
	public string ApproverUserId { get; init; } = "";
	public string TaskCompanyAssignmentId { get; init; } = "";
	public string ApproverType { get; init; } = "";
	public bool IsPrimary { get; init; }
	public int Sequence { get; init; }
	
	public TaskCompanyAssignmentState? TaskCompanyAssignment { get; init; }
	
	
}
