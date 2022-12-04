using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record PaymentTransactionState : BaseEntity
{
	public string EnrolledPayeeId { get; init; } = "";
	public string? BatchId { get; init; }
	public DateTime? TransmissionDate { get; init; }
	public string DocumentNumber { get; init; } = "";
	public DateTime DocumentDate { get; init; }
	public decimal DocumentAmount { get; init; }
	public string CheckNumber { get; init; } = "";
	public string PaymentType { get; init; } = "";
	public string TextFileName { get; init; } = "";
	public string PdfReport { get; init; } = "";
	public bool Emailed { get; init; }
	public string GroupCode { get; init; } = "";
	public string Status { get; init; } = "";
	public decimal IfcaBatchNumber { get; init; }
	public decimal IfcaLineNumber { get; init; }
	public int EmailSentCount { get; init; }
	public DateTime? EmailSentDateTime { get; init; }
	public bool IsForSending { get; init; }
	
	public EnrolledPayeeState? EnrolledPayee { get; init; }
	public BatchState? Batch { get; init; }
	
	
}
