using CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Project.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.Project;

[Authorize(Policy = Permission.Project.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProjectViewModel Project { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectByIdQuery(id)), Project);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (Project.LogoForm != null && await UploadFile<ProjectViewModel>(WebConstants.Project, nameof(Project.Logo), Project.Id, Project.LogoForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProjectCommand>(Project)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectDeliverable")
		{
			return AddProjectDeliverable();
		}
		if (AsyncAction == "RemoveProjectDeliverable")
		{
			return RemoveProjectDeliverable();
		}
		if (AsyncAction == "AddProjectMilestone")
		{
			return AddProjectMilestone();
		}
		if (AsyncAction == "RemoveProjectMilestone")
		{
			return RemoveProjectMilestone();
		}
		if (AsyncAction == "AddProjectPackage")
		{
			return AddProjectPackage();
		}
		if (AsyncAction == "RemoveProjectPackage")
		{
			return RemoveProjectPackage();
		}
		
		
        return Partial("_InputFieldsPartial", Project);
    }
	
	private IActionResult AddProjectDeliverable()
	{
		ModelState.Clear();
		if (Project!.ProjectDeliverableList == null) { Project!.ProjectDeliverableList = new List<ProjectDeliverableViewModel>(); }
		Project!.ProjectDeliverableList!.Add(new ProjectDeliverableViewModel() { ProjectId = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveProjectDeliverable()
	{
		ModelState.Clear();
		Project.ProjectDeliverableList = Project!.ProjectDeliverableList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}

	private IActionResult AddProjectMilestone()
	{
		ModelState.Clear();
		if (Project!.ProjectMilestoneList == null) { Project!.ProjectMilestoneList = new List<ProjectMilestoneViewModel>(); }
		Project!.ProjectMilestoneList!.Add(new ProjectMilestoneViewModel() { ProjectId = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveProjectMilestone()
	{
		ModelState.Clear();
		Project.ProjectMilestoneList = Project!.ProjectMilestoneList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}

	private IActionResult AddProjectPackage()
	{
		ModelState.Clear();
		if (Project!.ProjectPackageList == null) { Project!.ProjectPackageList = new List<ProjectPackageViewModel>(); }
		Project!.ProjectPackageList!.Add(new ProjectPackageViewModel() { ProjectId = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveProjectPackage()
	{
		ModelState.Clear();
		Project.ProjectPackageList = Project!.ProjectPackageList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}
	
}
