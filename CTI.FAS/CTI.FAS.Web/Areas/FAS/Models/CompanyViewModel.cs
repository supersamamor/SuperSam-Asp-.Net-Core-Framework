using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record CompanyViewModel : BaseViewModel
{	
	[Display(Name = "Database Connection Setup")]
	[Required]
	
	public string DatabaseConnectionSetupId { get; init; } = "";
	public string?  ForeignKeyDatabaseConnectionSetup { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Address Line 1")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddress { get; init; }
	[Display(Name = "Address Line 2")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddressSecondLine { get; init; }
	[Display(Name = "Description")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityDescription { get; init; }
	[Display(Name = "Short Name")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityShortName { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "TINNo")]
	[StringLength(17, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNo { get; init; }
	[Display(Name = "Submit Place")]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SubmitPlace { get; init; }
	[Display(Name = "Submit Deadline")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? SubmitDeadline { get; init; }
	[Display(Name = "Email / Telephone Number")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EmailTelephoneNumber { get; init; }
	[Display(Name = "Image Logo")]
	
	public IFormFile? ImageLogoForm { get; set; }public string? ImageLogo { get; init; }
	public string? GeneratedImageLogoPath
	{
		get
		{
			return this.ImageLogoForm?.FileName == null ? this.ImageLogo : "\\" + WebConstants.Company + "\\" + this.Id + "\\" + nameof(this.ImageLogo) + "\\" + this.ImageLogoForm!.FileName;
		}
	}
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
	public DatabaseConnectionSetupViewModel? DatabaseConnectionSetup { get; init; }
		
	public IList<ProjectViewModel>? ProjectList { get; set; }
	public IList<UserEntityViewModel>? UserEntityList { get; set; }
	public IList<EnrolledPayeeViewModel>? EnrolledPayeeList { get; set; }
	
}
