using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.MailSetting;

[Authorize(Policy = Permission.MailSetting.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
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

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteMailSettingCommand { Id = MailSetting.Id }), "Index");
    }
}
