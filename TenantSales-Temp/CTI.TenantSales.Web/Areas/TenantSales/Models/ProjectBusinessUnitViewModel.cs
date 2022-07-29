using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record ProjectBusinessUnitViewModel : BaseViewModel
{	
	[Display(Name = "Business Unit")]
	[Required]
	
	public string BusinessUnitId { get; init; } = "";
	public string?  ForeignKeyBusinessUnit { get; set; }
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public BusinessUnitViewModel? BusinessUnit { get; init; }
	public ProjectViewModel? Project { get; init; }
		
	
}
