using CTI.TenantSales.Application.Features.TenantSales.Approval.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Approval.Queries;
using CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.DatabaseConnectionSetup;

[Authorize(Policy = Permission.DatabaseConnectionSetup.Approve)]
public class ApproveModel : BasePageModel<ApproveModel>
{
    [BindProperty]
    public DatabaseConnectionSetupViewModel DatabaseConnectionSetup { get; set; } = new();
    [BindProperty]
    public string? ApprovalStatus { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetApprovalStatusPerApproverByIdQuery(id))).Select(l => ApprovalStatus = l);
        return await PageFrom(async () => await Mediatr.Send(new GetDatabaseConnectionSetupByIdQuery(id)), DatabaseConnectionSetup);
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
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new ApproveCommand(DatabaseConnectionSetup.Id, "")), "Approve", true);
    }
    private async Task<IActionResult> Reject()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new RejectCommand(DatabaseConnectionSetup.Id, "")), "Approve", true);
    }
}
