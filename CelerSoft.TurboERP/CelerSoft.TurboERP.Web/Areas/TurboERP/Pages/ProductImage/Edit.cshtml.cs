using CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.ProductImage;

[Authorize(Policy = Permission.ProductImage.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProductImageViewModel ProductImage { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProductImageByIdQuery(id)), ProductImage);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (ProductImage.PathForm != null && await UploadFile<ProductImageViewModel>(WebConstants.ProductImage, nameof(ProductImage.Path), ProductImage.Id, ProductImage.PathForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProductImageCommand>(ProductImage)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ProductImage);
    }
	
}
