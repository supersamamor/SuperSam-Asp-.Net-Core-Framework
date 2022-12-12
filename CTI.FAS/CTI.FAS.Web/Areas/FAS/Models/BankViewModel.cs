using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record BankViewModel : BaseViewModel
{	
	[Display(Name = "Entity")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Bank")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BankName { get; init; }
	[Display(Name = "Bank Code")]
	[StringLength(12, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BankCode { get; init; }
	[Display(Name = "Account Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AccountName { get; init; }
	[Display(Name = "Account Type")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AccountType { get; init; }
	[Display(Name = "Account Number")]
	[StringLength(14, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AccountNumber { get; init; }	
	[Display(Name = "Delivery Corporation Branch")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DeliveryCorporationBranch { get; init; }
	[Display(Name = "Signatory Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryType { get; init; }
	[Display(Name = "Signatory 1")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Signatory1 { get; init; }
	[Display(Name = "Signatory 2")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Signatory2 { get; init; }
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	
}
