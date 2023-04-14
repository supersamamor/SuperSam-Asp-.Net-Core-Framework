using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record ProductImageViewModel : BaseViewModel
{	
	[Display(Name = "Product")]
	
	public string? ProductId { get; init; }
	public string?  ForeignKeyProduct { get; set; }
	[Display(Name = "Path")]
	[Required]
	
	public IFormFile? PathForm { get; set; }public string? Path { get; init; } = "";
	public string? GeneratedPathPath
	{
		get
		{
			return this.PathForm?.FileName == null ? this.Path : "\\" + WebConstants.ProductImage + "\\" + this.Id + "\\" + nameof(this.Path) + "\\" + this.PathForm!.FileName;
		}
	}
	
	public DateTime LastModifiedDate { get; set; }
	public ProductViewModel? Product { get; init; }
		
	
}
