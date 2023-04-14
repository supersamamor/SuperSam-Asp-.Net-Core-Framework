using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Order;

[Authorize(Policy = Permission.Order.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public OrderViewModel Order { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOrderByIdQuery(id)), Order);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteOrderCommand { Id = Order.Id }), "Index");
    }
}
