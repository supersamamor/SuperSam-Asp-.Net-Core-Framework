using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.MilestoneStage;

[Authorize(Policy = Permission.MilestoneStage.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public MilestoneStageViewModel MilestoneStage { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetMilestoneStageByIdQuery(id)), MilestoneStage);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditMilestoneStageCommand>(MilestoneStage)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", MilestoneStage);
    }
	
	private IActionResult AddProjectMilestone()
	{
		ModelState.Clear();
		if (MilestoneStage!.ProjectMilestoneList == null) { MilestoneStage!.ProjectMilestoneList = new List<ProjectMilestoneViewModel>(); }
		MilestoneStage!.ProjectMilestoneList!.Add(new ProjectMilestoneViewModel() { MilestoneStageId = MilestoneStage.Id });
		return Partial("_InputFieldsPartial", MilestoneStage);
	}
	private IActionResult RemoveProjectMilestone()
	{
		ModelState.Clear();
		MilestoneStage.ProjectMilestoneList = MilestoneStage!.ProjectMilestoneList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", MilestoneStage);
	}

	private IActionResult AddProjectMilestoneHistory()
	{
		ModelState.Clear();
		if (MilestoneStage!.ProjectMilestoneHistoryList == null) { MilestoneStage!.ProjectMilestoneHistoryList = new List<ProjectMilestoneHistoryViewModel>(); }
		MilestoneStage!.ProjectMilestoneHistoryList!.Add(new ProjectMilestoneHistoryViewModel() { MilestoneStageId = MilestoneStage.Id });
		return Partial("_InputFieldsPartial", MilestoneStage);
	}
	private IActionResult RemoveProjectMilestoneHistory()
	{
		ModelState.Clear();
		MilestoneStage.ProjectMilestoneHistoryList = MilestoneStage!.ProjectMilestoneHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", MilestoneStage);
	}
	
}
