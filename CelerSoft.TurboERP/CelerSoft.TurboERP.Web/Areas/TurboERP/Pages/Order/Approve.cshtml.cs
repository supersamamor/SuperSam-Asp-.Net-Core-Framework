using CelerSoft.TurboERP.Application.Features.TurboERP.Approval.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Approval.Queries;
using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Order;

[Authorize(Policy = Permission.Order.Approve)]
public class ApproveModel : BasePageModel<ApproveModel>
{
    [BindProperty]
    public OrderViewModel Order { get; set; } = new();
    [BindProperty]
    public string? ApprovalStatus { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetApprovalStatusPerApproverByIdQuery(id))).Select(l => ApprovalStatus = l);
        return await PageFrom(async () => await Mediatr.Send(new GetOrderByIdQuery(id)), Order);
    }

    public async Task<IActionResult> OnPost(string handler)
    {
        if (handler == "Approve")
        {
            return await Approve();
        }
        else if (handler == "Reject")
        {
            return await Reject();
        }
        return Page();
    }
    private async Task<IActionResult> Approve()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new ApproveCommand(Order.Id, "")), "Approve", true);
    }
    private async Task<IActionResult> Reject()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new RejectCommand(Order.Id, "")), "Approve", true);
    }
}
