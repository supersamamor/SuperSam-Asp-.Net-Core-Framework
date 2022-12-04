using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record EnrolledPayeeViewModel : BaseViewModel
{	
	[Display(Name = "Entity")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Creditor")]
	[Required]
	
	public string CreditorId { get; init; } = "";
	public string?  ForeignKeyCreditor { get; set; }
	[Display(Name = "Account Number")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountNumber { get; init; } = "";
	[Display(Name = "Account Type")]
	[Required]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountType { get; init; } = "";
	[Display(Name = "Status")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
	public CreditorViewModel? Creditor { get; init; }
		
	public IList<PaymentTransactionViewModel>? PaymentTransactionList { get; set; }
	public IList<EnrolledPayeeEmailViewModel>? EnrolledPayeeEmailList { get; set; }
	
}
