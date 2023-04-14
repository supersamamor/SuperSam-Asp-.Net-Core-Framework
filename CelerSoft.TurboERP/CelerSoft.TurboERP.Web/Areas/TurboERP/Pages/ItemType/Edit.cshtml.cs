using CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.ItemType;

[Authorize(Policy = Permission.ItemType.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ItemTypeViewModel ItemType { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetItemTypeByIdQuery(id)), ItemType);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditItemTypeCommand>(ItemType)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ItemType);
    }
	
}
