using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record SupplierViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Company { get; init; } = "";
	[Display(Name = "TIN Number")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNumber { get; init; }
	[Display(Name = "Address")]
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Address { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<SupplierContactPersonViewModel>? SupplierContactPersonList { get; set; }
	public IList<SupplierQuotationViewModel>? SupplierQuotationList { get; set; }
	
}
