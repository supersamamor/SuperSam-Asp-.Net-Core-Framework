using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Product;

[Authorize(Policy = Permission.Product.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProductViewModel Product { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddProductCommand>(Product)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProductImage")
		{
			return AddProductImage();
		}
		if (AsyncAction == "RemoveProductImage")
		{
			return RemoveProductImage();
		}	
        return Partial("_InputFieldsPartial", Product);
    }
	
	private IActionResult AddProductImage()
	{
		ModelState.Clear();
		if (Product!.ProductImageList == null) { Product!.ProductImageList = new List<ProductImageViewModel>(); }
		Product!.ProductImageList!.Add(new ProductImageViewModel() { ProductId = Product.Id });
		return Partial("_InputFieldsPartial", Product);
	}
	private IActionResult RemoveProductImage()
	{
		ModelState.Clear();
		Product.ProductImageList = Product!.ProductImageList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Product);
	}


}
