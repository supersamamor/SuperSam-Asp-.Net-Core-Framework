using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.Frequency;

[Authorize(Policy = Permission.Frequency.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public FrequencyViewModel Frequency { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddFrequencyCommand>(Frequency)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectMilestone")
		{
			return AddProjectMilestone();
		}
		if (AsyncAction == "RemoveProjectMilestone")
		{
			return RemoveProjectMilestone();
		}
		if (AsyncAction == "AddProjectMilestoneHistory")
		{
			return AddProjectMilestoneHistory();
		}
		if (AsyncAction == "RemoveProjectMilestoneHistory")
		{
			return RemoveProjectMilestoneHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Frequency);
    }
	
	private IActionResult AddProjectMilestone()
	{
		ModelState.Clear();
		if (Frequency!.ProjectMilestoneList == null) { Frequency!.ProjectMilestoneList = new List<ProjectMilestoneViewModel>(); }
		Frequency!.ProjectMilestoneList!.Add(new ProjectMilestoneViewModel() { FrequencyId = Frequency.Id });
		return Partial("_InputFieldsPartial", Frequency);
	}
	private IActionResult RemoveProjectMilestone()
	{
		ModelState.Clear();
		Frequency.ProjectMilestoneList = Frequency!.ProjectMilestoneList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Frequency);
	}

	private IActionResult AddProjectMilestoneHistory()
	{
		ModelState.Clear();
		if (Frequency!.ProjectMilestoneHistoryList == null) { Frequency!.ProjectMilestoneHistoryList = new List<ProjectMilestoneHistoryViewModel>(); }
		Frequency!.ProjectMilestoneHistoryList!.Add(new ProjectMilestoneHistoryViewModel() { FrequencyId = Frequency.Id });
		return Partial("_InputFieldsPartial", Frequency);
	}
	private IActionResult RemoveProjectMilestoneHistory()
	{
		ModelState.Clear();
		Frequency.ProjectMilestoneHistoryList = Frequency!.ProjectMilestoneHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Frequency);
	}
	
}
