using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Approval.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ApproverSetup;

[Authorize(Policy = Permission.ApproverSetup.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ApproverSetupViewModel ApproverSetup { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetApproverSetupByIdQuery(id)), ApproverSetup);
    }
}
