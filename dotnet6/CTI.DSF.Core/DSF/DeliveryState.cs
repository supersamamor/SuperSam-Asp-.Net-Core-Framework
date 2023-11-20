using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record DeliveryState : BaseEntity
{
	public string TaskCompanyAssignmentId { get; init; } = "";
	public string DeliveryCode { get; init; } = "";
	public string AssignmentId { get; init; } = "";
	public DateTime DueDate { get; init; }
	public string Status { get; init; } = "";
	public string? DeliveryAttachment { get; init; } = "";
	public string Remarks { get; init; } = "";
	public string HolidayTag { get; init; } = "";
	public DateTime? SubmittedDate { get; init; }
	public string? SubmittedBy { get; init; }
	public DateTime? ReviewedDate { get; init; }
	public string? ReviewedBy { get; init; }
	public DateTime? ApprovedDate { get; init; }
	public string? ApprovedBy { get; init; }
	public DateTime? RejectedDate { get; init; }
	public string? RejectedBy { get; init; }
	public DateTime? CancelledDate { get; init; }
	public string? CancelledBy { get; init; }
	
	public TaskCompanyAssignmentState? TaskCompanyAssignment { get; init; }
	public AssignmentState? Assignment { get; init; }
	
	public IList<DeliveryApprovalHistoryState>? DeliveryApprovalHistoryList { get; set; }
	
}
