using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record CreditorViewModel : BaseViewModel
{	
	[Display(Name = "Entity")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Creditor Account")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CreditorAccount { get; init; } = "";
	[Display(Name = "Account Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountName { get; init; } = "";
	[Display(Name = "Account Type")]
	[Required]
	[StringLength(2, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountType { get; init; } = "";
	[Display(Name = "Account Number")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountNumber { get; init; } = "";
	[Display(Name = "Payee Account Name")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountName { get; init; } = "";
	[Display(Name = "Payee Account Number")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountNumber { get; init; } = "";
	[Display(Name = "Email")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get; init; } = "";
	[Display(Name = "Payee Account Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountCode { get; init; } = "";
	[Display(Name = "Payee Account TIN")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountTIN { get; init; } = "";
	[Display(Name = "Payee Account Address")]
	[Required]
	[StringLength(60, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountAddress { get; init; } = "";
	[Display(Name = "Status")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	public IList<CheckReleaseOptionViewModel>? CheckReleaseOptionList { get; set; }
	public IList<GeneratedViewModel>? GeneratedList { get; set; }
	public IList<CreditorEmailViewModel>? CreditorEmailList { get; set; }
	
}
