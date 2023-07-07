using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.Approval.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.Approval.Queries;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.ModPlaceHolder;

[Authorize(Policy = Permission.ModPlaceHolder.Approve)]
public class ApproveModel : BasePageModel<ApproveModel>
{
    [BindProperty]
    public ModPlaceHolderViewModel ModPlaceHolder { get; set; } = new();
    [BindProperty]
    public string? ApprovalStatus { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetApprovalStatusPerApproverByIdQuery(id))).Select(l => ApprovalStatus = l);
        return await PageFrom(async () => await Mediatr.Send(new GetModPlaceHolderByIdQuery(id)), ModPlaceHolder);
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
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new ApproveCommand(ModPlaceHolder.Id, "")), "Approve", true);
    }
    private async Task<IActionResult> Reject()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new RejectCommand(ModPlaceHolder.Id, "")), "Approve", true);
    }
}
