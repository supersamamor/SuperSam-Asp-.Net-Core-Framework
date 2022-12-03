using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record ProjectViewModel : BaseViewModel
{	
	[Display(Name = "Entity")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Address")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectAddress { get; init; }
	[Display(Name = "Location")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Location { get; init; }
	[Display(Name = "Land Area")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LandArea { get; init; }
	[Display(Name = "GLA")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal GLA { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	public IList<TenantViewModel>? TenantList { get; set; }
	
}
