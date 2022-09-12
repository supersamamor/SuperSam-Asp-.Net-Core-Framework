using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectHistory;

[Authorize(Policy = Permission.ProjectHistory.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProjectHistoryViewModel ProjectHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectHistoryByIdQuery(id)), ProjectHistory);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (ProjectHistory.LogoForm != null && await UploadFile<ProjectHistoryViewModel>(WebConstants.ProjectHistory, nameof(ProjectHistory.Logo), ProjectHistory.Id, ProjectHistory.LogoForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProjectHistoryCommand>(ProjectHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectDeliverableHistory")
		{
			return AddProjectDeliverableHistory();
		}
		if (AsyncAction == "RemoveProjectDeliverableHistory")
		{
			return RemoveProjectDeliverableHistory();
		}
		if (AsyncAction == "AddProjectMilestoneHistory")
		{
			return AddProjectMilestoneHistory();
		}
		if (AsyncAction == "RemoveProjectMilestoneHistory")
		{
			return RemoveProjectMilestoneHistory();
		}
		if (AsyncAction == "AddProjectPackageHistory")
		{
			return AddProjectPackageHistory();
		}
		if (AsyncAction == "RemoveProjectPackageHistory")
		{
			return RemoveProjectPackageHistory();
		}
		
		
        return Partial("_InputFieldsPartial", ProjectHistory);
    }
	
	private IActionResult AddProjectDeliverableHistory()
	{
		ModelState.Clear();
		if (ProjectHistory!.ProjectDeliverableHistoryList == null) { ProjectHistory!.ProjectDeliverableHistoryList = new List<ProjectDeliverableHistoryViewModel>(); }
		ProjectHistory!.ProjectDeliverableHistoryList!.Add(new ProjectDeliverableHistoryViewModel() { ProjectHistoryId = ProjectHistory.Id });
		return Partial("_InputFieldsPartial", ProjectHistory);
	}
	private IActionResult RemoveProjectDeliverableHistory()
	{
		ModelState.Clear();
		ProjectHistory.ProjectDeliverableHistoryList = ProjectHistory!.ProjectDeliverableHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ProjectHistory);
	}

	private IActionResult AddProjectMilestoneHistory()
	{
		ModelState.Clear();
		if (ProjectHistory!.ProjectMilestoneHistoryList == null) { ProjectHistory!.ProjectMilestoneHistoryList = new List<ProjectMilestoneHistoryViewModel>(); }
		ProjectHistory!.ProjectMilestoneHistoryList!.Add(new ProjectMilestoneHistoryViewModel() { ProjectHistoryId = ProjectHistory.Id });
		return Partial("_InputFieldsPartial", ProjectHistory);
	}
	private IActionResult RemoveProjectMilestoneHistory()
	{
		ModelState.Clear();
		ProjectHistory.ProjectMilestoneHistoryList = ProjectHistory!.ProjectMilestoneHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ProjectHistory);
	}

	private IActionResult AddProjectPackageHistory()
	{
		ModelState.Clear();
		if (ProjectHistory!.ProjectPackageHistoryList == null) { ProjectHistory!.ProjectPackageHistoryList = new List<ProjectPackageHistoryViewModel>(); }
		ProjectHistory!.ProjectPackageHistoryList!.Add(new ProjectPackageHistoryViewModel() { ProjectHistoryId = ProjectHistory.Id });
		return Partial("_InputFieldsPartial", ProjectHistory);
	}
	private IActionResult RemoveProjectPackageHistory()
	{
		ModelState.Clear();
		ProjectHistory.ProjectPackageHistoryList = ProjectHistory!.ProjectPackageHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ProjectHistory);
	}
	
}
