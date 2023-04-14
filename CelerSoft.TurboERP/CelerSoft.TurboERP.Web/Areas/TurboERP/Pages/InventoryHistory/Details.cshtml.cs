using CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.InventoryHistory;

[Authorize(Policy = Permission.InventoryHistory.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public InventoryHistoryViewModel InventoryHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetInventoryHistoryByIdQuery(id)), InventoryHistory);
    }
}
