using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Commands;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.MailRecipient;

[Authorize(Policy = Permission.MailRecipient.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public MailRecipientViewModel MailRecipient { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddMailRecipientCommand>(MailRecipient)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", MailRecipient);
    }
	
}
