using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ApplicationConfigurationViewModel : BaseViewModel
{	
	[Display(Name = "Logo")]
	[Required]
	
	public IFormFile? LogoForm { get; set; }public string? Logo { get; init; } = "";
	public string? GeneratedLogoPath
	{
		get
		{
			return this.LogoForm?.FileName == null ? this.Logo : "\\" + WebConstants.ApplicationConfiguration + "\\" + this.Id + "\\" + nameof(this.Logo) + "\\" + this.LogoForm!.FileName;
		}
	}
	[Display(Name = "Address Line 1")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AddressLineOne { get; init; } = "";
	[Display(Name = "Address Line 2")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AddressLineTwo { get; init; }
	[Display(Name = "Organization Overview")]
	[Required]
	
	public string OrganizationOverview { get; init; } = "";
	[Display(Name = "Document Footer")]
	[Required]
	
	public string DocumentFooter { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
