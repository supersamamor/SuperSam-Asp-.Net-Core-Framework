using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record TenantPOSViewModel : BaseViewModel
{	
	[Display(Name = "Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Tenant")]
	[Required]
	
	public string TenantId { get; init; } = "";
	public string?  ForeignKeyTenant { get; set; }
	[Display(Name = "Serial Number")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SerialNumber { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public TenantViewModel? Tenant { get; init; }
		
	public IList<TenantPOSSalesViewModel>? TenantPOSSalesList { get; set; }
	
}
