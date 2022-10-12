using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ScheduleFrequency;

[Authorize(Policy = Permission.ScheduleFrequency.Edit)]
public class EditModel : BasePageModel<EditModel>
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditScheduleFrequencyCommand>(ScheduleFrequency)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddScheduleFrequencyParameterSetup")
		{
			return AddScheduleFrequencyParameterSetup();
		}
		if (AsyncAction == "RemoveScheduleFrequencyParameterSetup")
		{
			return RemoveScheduleFrequencyParameterSetup();
		}
		
		
        return Partial("_InputFieldsPartial", ScheduleFrequency);
    }
	
	private IActionResult AddScheduleFrequencyParameterSetup()
	{
		ModelState.Clear();
		if (ScheduleFrequency!.ScheduleFrequencyParameterSetupList == null) { ScheduleFrequency!.ScheduleFrequencyParameterSetupList = new List<ScheduleFrequencyParameterSetupViewModel>(); }
		ScheduleFrequency!.ScheduleFrequencyParameterSetupList!.Add(new ScheduleFrequencyParameterSetupViewModel() { ScheduleFrequencyId = ScheduleFrequency.Id });
		return Partial("_InputFieldsPartial", ScheduleFrequency);
	}
	private IActionResult RemoveScheduleFrequencyParameterSetup()
	{
		ModelState.Clear();
		ScheduleFrequency.ScheduleFrequencyParameterSetupList = ScheduleFrequency!.ScheduleFrequencyParameterSetupList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ScheduleFrequency);
	}
	
}
