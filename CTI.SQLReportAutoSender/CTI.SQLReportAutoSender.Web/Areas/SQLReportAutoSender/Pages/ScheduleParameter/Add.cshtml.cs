using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Commands;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ScheduleParameter;

[Authorize(Policy = Permission.ScheduleParameter.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ScheduleParameterViewModel ScheduleParameter { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		this.ScheduleParameter.ReportScheduleSettingList = new List<ReportScheduleSettingViewModel>() { new ReportScheduleSettingViewModel() { ScheduleParameterId = this.ScheduleParameter.Id} };
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddScheduleParameterCommand>(ScheduleParameter)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", ScheduleParameter);
    }
	
	private IActionResult AddScheduleFrequencyParameterSetup()
	{
		ModelState.Clear();
		if (ScheduleParameter!.ScheduleFrequencyParameterSetupList == null) { ScheduleParameter!.ScheduleFrequencyParameterSetupList = new List<ScheduleFrequencyParameterSetupViewModel>(); }
		ScheduleParameter!.ScheduleFrequencyParameterSetupList!.Add(new ScheduleFrequencyParameterSetupViewModel() { ScheduleParameterId = ScheduleParameter.Id });
		return Partial("_InputFieldsPartial", ScheduleParameter);
	}
	private IActionResult RemoveScheduleFrequencyParameterSetup()
	{
		ModelState.Clear();
		ScheduleParameter.ScheduleFrequencyParameterSetupList = ScheduleParameter!.ScheduleFrequencyParameterSetupList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ScheduleParameter);
	}
	
}
