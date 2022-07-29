using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record CategoryViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(80, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Classification")]
	[Required]
	
	public string ClassificationId { get; init; } = "";
	public string?  ForeignKeyClassification { get; set; }
	[Display(Name = "Code")]
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ClassificationViewModel? Classification { get; init; }
		
	
}
