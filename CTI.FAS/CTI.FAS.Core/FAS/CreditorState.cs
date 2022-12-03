using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record CreditorState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string CreditorAccount { get; init; } = "";
	public string AccountName { get; init; } = "";
	public string AccountType { get; init; } = "";
	public string AccountNumber { get; init; } = "";
	public string PayeeAccountName { get; init; } = "";
	public string PayeeAccountNumber { get; init; } = "";
	public string Email { get; init; } = "";
	public string PayeeAccountCode { get; init; } = "";
	public string PayeeAccountTIN { get; init; } = "";
	public string PayeeAccountAddress { get; init; } = "";
	public string? Status { get; init; }
	
	public CompanyState? Company { get; init; }
	
	public IList<CheckReleaseOptionState>? CheckReleaseOptionList { get; set; }
	public IList<GeneratedState>? GeneratedList { get; set; }
	public IList<CreditorEmailState>? CreditorEmailList { get; set; }
	
}
