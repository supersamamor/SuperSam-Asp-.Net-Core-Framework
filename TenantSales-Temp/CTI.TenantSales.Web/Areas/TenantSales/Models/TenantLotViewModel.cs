using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record TenantLotViewModel : BaseViewModel
{	
	[Display(Name = "Area")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Area { get; init; }
	[Display(Name = "LotNo")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LotNo { get; init; }
	[Display(Name = "Tenant")]
	[Required]
	
	public string TenantId { get; init; } = "";
	public string?  ForeignKeyTenant { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public TenantViewModel? Tenant { get; init; }
		
	
}
