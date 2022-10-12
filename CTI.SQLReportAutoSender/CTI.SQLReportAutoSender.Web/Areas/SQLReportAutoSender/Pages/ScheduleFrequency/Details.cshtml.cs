using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ScheduleFrequency;

[Authorize(Policy = Permission.ScheduleFrequency.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
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
}
