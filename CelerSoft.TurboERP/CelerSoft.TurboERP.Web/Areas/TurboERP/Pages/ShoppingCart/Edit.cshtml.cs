using CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.ShoppingCart;

[Authorize(Policy = Permission.ShoppingCart.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ShoppingCartViewModel ShoppingCart { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetShoppingCartByIdQuery(id)), ShoppingCart);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditShoppingCartCommand>(ShoppingCart)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ShoppingCart);
    }
	
}
