using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.CustomSchedule;

[Authorize(Policy = Permission.CustomSchedule.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public CustomScheduleViewModel CustomSchedule { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetCustomScheduleByIdQuery(id)), CustomSchedule);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditCustomScheduleCommand>(CustomSchedule)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", CustomSchedule);
    }
	
}
