using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ScheduleFrequency;

[Authorize(Policy = Permission.ScheduleFrequency.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ScheduleFrequencyViewModel ScheduleFrequency { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetScheduleFrequencyByIdQuery(id)), ScheduleFrequency);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteScheduleFrequencyCommand { Id = ScheduleFrequency.Id }), "Index");
    }
}
