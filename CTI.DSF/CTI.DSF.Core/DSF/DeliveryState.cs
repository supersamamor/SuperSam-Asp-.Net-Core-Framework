using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record DeliveryState : BaseEntity
{
	public string? DeliveryCode { get; init; }
	public string AssignmentId { get; init; } = "";
	public DateTime DueDate { get; init; }
	public string Status { get; init; } = "";
	public string? DeliveryAttachment { get; init; } = "";
	public string Remarks { get; init; } = "";
	public string HolidayTag { get; init; } = "";
	
	public AssignmentState? Assignment { get; init; }
	
	
}
