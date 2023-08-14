using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record DeliveryState : BaseEntity
{
	public string DeliveryCode { get; set; } = "";
	public string AssignmentCode { get; init; } = "";
	public string? TaskDescription { get; init; }
	public DateTime? DueDate { get; init; }
	public string? DeliveryAttachment { get; init; } = "";
	public string? Status { get; init; }
	public string? Remarks { get; init; }

    public string? AssignmentId { get; init; }
    public AssignmentState? Assignment { get; init; }

	public void GenerateDeliveryCode()
	{
		DeliveryCode = Guid.NewGuid().ToString();
	}
	
	
}
