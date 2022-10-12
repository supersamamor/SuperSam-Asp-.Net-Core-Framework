using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ReportInbox;

[Authorize(Policy = Permission.ReportInbox.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ReportInboxViewModel ReportInbox { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportInboxByIdQuery(id)), ReportInbox);
    }
}
