using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record RevalidateViewModel : BaseViewModel
{	
	[Display(Name = "Sales Date")]
	[Required]
	public DateTime SalesDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Tenant")]
	
	public string? TenantId { get; init; }
	public string?  ForeignKeyTenant { get; set; }
	[Display(Name = "Status")]

	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; } = "";
	[Display(Name = "Processing Remarks")]
	
	public string? ProcessingRemarks { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
	public TenantViewModel? Tenant { get; init; }
		
	
}
