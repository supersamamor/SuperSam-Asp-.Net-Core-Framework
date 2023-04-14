using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.PurchaseItem;

[Authorize(Policy = Permission.PurchaseItem.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public PurchaseItemViewModel PurchaseItem { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetPurchaseItemByIdQuery(id)), PurchaseItem);
    }
}
