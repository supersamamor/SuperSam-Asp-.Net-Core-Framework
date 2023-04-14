using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Product;

[Authorize(Policy = Permission.Product.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProductViewModel Product { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetProductByIdQuery(id)), Product);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProductCommand>(Product)), "Details", true);
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
