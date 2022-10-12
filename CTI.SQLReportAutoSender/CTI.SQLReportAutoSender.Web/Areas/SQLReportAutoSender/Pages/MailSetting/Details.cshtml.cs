using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.MailSetting;

[Authorize(Policy = Permission.MailSetting.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public MailSettingViewModel MailSetting { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetMailSettingByIdQuery(id)), MailSetting);
    }
}
