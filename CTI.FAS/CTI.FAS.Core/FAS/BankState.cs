using CTI.Common.Core.Base.Models;
namespace CTI.FAS.Core.FAS;
public record BankState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string? BankName { get; init; }
	public string? BankCode { get; init; }
	public string? AccountName { get; init; }
	public string? AccountType { get; init; }
	public string? AccountNumber { get; init; }
	public string? DeliveryCorporationBranch { get; init; }
	public string? SignatoryType { get; init; }
	public string? Signatory1 { get; init; }
	public string? Signatory2 { get; init; }
	public CompanyState? Company { get; init; }
	public string? DisplayName
	{
		get
		{ 
			return this.BankName + " - " + this.AccountNumber;
		}
	}
}
