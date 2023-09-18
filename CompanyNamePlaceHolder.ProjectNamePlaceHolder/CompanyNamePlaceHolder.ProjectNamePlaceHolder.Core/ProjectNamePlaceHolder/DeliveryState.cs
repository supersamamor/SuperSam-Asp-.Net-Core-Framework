using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record DeliveryState : BaseEntity
{
	public string? ApproverRemarks { get; init; }
	public string? Status { get; init; }
	public string? EndorserRemarks { get; init; }
	public DateTime? EndorsedDate { get; init; }
	public string? ApprovedTag { get; init; }
	public string? EndorsedTag { get; init; }
	public string DeliveryCode { get; init; } = "";
	public DateTime ActualDeliveryDate { get; init; }
	public string? DeliveryAttachment { get; init; } = "";
	public string? ActualDeliveryRemarks { get; init; }
	public string AssignmentCode { get; init; } = "";
	public DateTime? DueDate { get; init; }
	
	public AssignmentState? Assignment { get; init; }
	
	
}
