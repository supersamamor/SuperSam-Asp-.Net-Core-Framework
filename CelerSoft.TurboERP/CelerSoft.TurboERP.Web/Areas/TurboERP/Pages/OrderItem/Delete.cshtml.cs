using CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.OrderItem;

[Authorize(Policy = Permission.OrderItem.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public OrderItemViewModel OrderItem { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOrderItemByIdQuery(id)), OrderItem);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteOrderItemCommand { Id = OrderItem.Id }), "Index");
    }
}
