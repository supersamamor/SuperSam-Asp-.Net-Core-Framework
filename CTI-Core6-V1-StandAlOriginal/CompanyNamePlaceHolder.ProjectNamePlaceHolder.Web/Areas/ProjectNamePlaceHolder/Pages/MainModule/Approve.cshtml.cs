using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModule;

[Authorize(Policy = Permission.MainModule.Approve)]
public class ApproveModel : BasePageModel<ApproveModel>
{
    [BindProperty]
    public MainModuleViewModel MainModule { get; set; } = new();
    [BindProperty]
    public string? ApprovalStatus { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetApprovalStatusPerApproverByIdQuery(id))).Select(l => ApprovalStatus = l);
        return await PageFrom(async () => await Mediatr.Send(new GetMainModuleByIdQuery(id)), MainModule);
    }

    public async Task<IActionResult> OnPost(string handler)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
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
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new ApproveCommand(MainModule.Id, "")), "Approve", true);
    }
    private async Task<IActionResult> Reject()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new RejectCommand(MainModule.Id, "")), "Approve", true);
    }
}
