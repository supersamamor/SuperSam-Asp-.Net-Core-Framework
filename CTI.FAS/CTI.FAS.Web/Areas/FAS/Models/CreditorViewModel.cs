using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record CreditorViewModel : BaseViewModel
{	
	[Display(Name = "Creditor Account")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CreditorAccount { get; init; } = "";
	[Display(Name = "Account Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountName { get; init; } = "";
	[Display(Name = "Account Name")]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PayeeAccountLongDescription { get; init; }
	[Display(Name = "Account Code")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PayeeAccountCode { get; init; }
	[Display(Name = "Account TIN")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PayeeAccountTIN { get; init; }
	[Display(Name = "Account Address")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountAddress { get; init; } = "";
	[Display(Name = "Email")]
	[Required]
	[StringLength(70, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get; init; } = "";
	[Display(Name = "Database Connection Setup")]
	[Required]
	
	public string DatabaseConnectionSetupId { get; init; } = "";
	public string?  ForeignKeyDatabaseConnectionSetup { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public DatabaseConnectionSetupViewModel? DatabaseConnectionSetup { get; init; }
		
	public IList<EnrolledPayeeViewModel>? EnrolledPayeeList { get; set; }
	
}
