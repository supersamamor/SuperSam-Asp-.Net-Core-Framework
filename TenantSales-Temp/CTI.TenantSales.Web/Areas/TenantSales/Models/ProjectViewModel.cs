using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record ProjectViewModel : BaseViewModel
{	
	[Display(Name = "Company")]
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
												[Display(Name = "Project Short Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectShortName { get; init; }
				[Display(Name = "Sales Upload Folder")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SalesUploadFolder { get; init; }
							
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	public IList<TenantViewModel>? TenantList { get; set; }
	public IList<ProjectBusinessUnitViewModel>? ProjectBusinessUnitList { get; set; }
	public IList<LevelViewModel>? LevelList { get; set; }
	public IList<RevalidateViewModel>? RevalidateList { get; set; }
	
}
