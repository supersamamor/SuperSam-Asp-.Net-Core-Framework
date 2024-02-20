using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ClientViewModel : BaseViewModel
{	
	[Display(Name = "Full Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactPersonName { get; init; } = "";
	[Display(Name = "Position")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactPersonPosition { get; init; } = "";
	[Display(Name = "Company Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyName { get; init; }
	[Display(Name = "Company Description")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyDescription { get; init; }
	[Display(Name = "Address Line 1")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyAddressLineOne { get; init; } = "";
	[Display(Name = "Address Line 2")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyAddressLineTwo { get; init; }
	[Display(Name = "Email")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EmailAddress { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<ProjectViewModel>? ProjectList { get; set; }
	public IList<ProjectHistoryViewModel>? ProjectHistoryList { get; set; }
	
}
