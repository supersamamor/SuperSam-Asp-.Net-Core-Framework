using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Inventory;

[Authorize(Policy = Permission.Inventory.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public InventoryViewModel Inventory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetInventoryByIdQuery(id)), Inventory);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteInventoryCommand { Id = Inventory.Id }), "Index");
    }
}
