using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record OrderViewModel : BaseViewModel
{	
	[Display(Name = "Checked By")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CheckedByFullName { get; init; } = "";
	[Display(Name = "Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get; init; }
	[Display(Name = "Customer")]
	[Required]
	
	public string CustomerId { get; init; } = "";
	public string?  ForeignKeyCustomer { get; set; }
	[Display(Name = "Remarks")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Remarks { get; init; } = "";
	[Display(Name = "Shopper Username")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ShopperUsername { get; init; } = "";
	[Display(Name = "Status")]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CustomerViewModel? Customer { get; init; }
		
	public IList<OrderItemViewModel>? OrderItemList { get; set; }
	
}
