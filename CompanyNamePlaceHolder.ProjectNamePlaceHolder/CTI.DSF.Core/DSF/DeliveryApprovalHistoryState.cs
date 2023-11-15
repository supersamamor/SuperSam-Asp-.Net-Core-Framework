using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record DeliveryApprovalHistoryState : BaseEntity
{
	public string DeliveryId { get; init; } = "";
	public DateTime? TransactionDateTime { get; init; }
	public string? Status { get; init; }
	public string? TransactedBy { get; init; }
	public string Remarks { get; init; } = "";
	
	public DeliveryState? Delivery { get; init; }
	
	
}
