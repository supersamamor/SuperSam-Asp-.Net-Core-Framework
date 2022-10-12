using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ReportScheduleSetting;

[Authorize(Policy = Permission.ReportScheduleSetting.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ReportScheduleSettingViewModel ReportScheduleSetting { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportScheduleSettingByIdQuery(id)), ReportScheduleSetting);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteReportScheduleSettingCommand { Id = ReportScheduleSetting.Id }), "Index");
    }
}
