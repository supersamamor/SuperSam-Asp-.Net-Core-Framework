using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record LevelViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Has Percentage Sales Tenant")]
	[Required]
	public bool HasPercentageSalesTenant { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	public IList<TenantViewModel>? TenantList { get; set; }
	
}
