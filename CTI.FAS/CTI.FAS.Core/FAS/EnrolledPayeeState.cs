using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record EnrolledPayeeState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string CreditorId { get; init; } = "";
	public string PayeeAccountNumber { get; init; } = "";
	public string PayeeAccountType { get; init; } = "";
	public string? Status { get; init; }
	
	public CompanyState? Company { get; init; }
	public CreditorState? Creditor { get; init; }
	
	public IList<PaymentTransactionState>? PaymentTransactionList { get; set; }
	public IList<EnrolledPayeeEmailState>? EnrolledPayeeEmailList { get; set; }
	
}
