using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record GeneratedState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string CreditorId { get; init; } = "";
	public string BatchId { get; init; } = "";
	public string Name { get; init; } = "";
	public DateTime TransmissionDate { get; init; }
	public string DocumentNumber { get; init; } = "";
	public DateTime DocumentDate { get; init; }
	public decimal DocumentAmount { get; init; }
	public string CheckNumber { get; init; } = "";
	public string Release { get; init; } = "";
	public string PaymentType { get; init; } = "";
	public string TextFileName { get; init; } = "";
	public string PdfReport { get; init; } = "";
	public bool Emailed { get; init; }
	public string GroupCode { get; init; } = "";
	public string Status { get; init; } = "";
	
	public CompanyState? Company { get; init; }
	public CreditorState? Creditor { get; init; }
	public BatchState? Batch { get; init; }
	
	
}
