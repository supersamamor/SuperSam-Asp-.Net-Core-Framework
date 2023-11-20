using CTI.DSF.Application.Features.DSF.Approval.Commands;
using CTI.DSF.Application.Features.DSF.Approval.Queries;
using CTI.DSF.Application.Features.DSF.Delivery.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Delivery;

[Authorize(Policy = Permission.Delivery.Approve)]
public class ApproveModel : BasePageModel<ApproveModel>
{
    [BindProperty]
    public DeliveryViewModel Delivery { get; set; } = new();
    [BindProperty]
    public string? ApprovalStatus { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetApprovalStatusPerApproverByIdQuery(id))).Select(l => ApprovalStatus = l);
        return await PageFrom(async () => await Mediatr.Send(new GetDeliveryByIdQuery(id)), Delivery);
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
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new ApproveCommand(Delivery.Id, "")), "Approve", true);
    }
    private async Task<IActionResult> Reject()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new RejectCommand(Delivery.Id, "")), "Approve", true);
    }
}
