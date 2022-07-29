using CTI.TenantSales.Application.Features.TenantSales.Project.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Project.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Project;

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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProjectCommand>(Project)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectBusinessUnit")
		{
			return AddProjectBusinessUnit();
		}
		if (AsyncAction == "RemoveProjectBusinessUnit")
		{
			return RemoveProjectBusinessUnit();
		}
		if (AsyncAction == "AddLevel")
		{
			return AddLevel();
		}
		if (AsyncAction == "RemoveLevel")
		{
			return RemoveLevel();
		}
		
		
        return Partial("_InputFieldsPartial", Project);
    }
	
	private IActionResult AddProjectBusinessUnit()
	{
		ModelState.Clear();
		if (Project!.ProjectBusinessUnitList == null) { Project!.ProjectBusinessUnitList = new List<ProjectBusinessUnitViewModel>(); }
		Project!.ProjectBusinessUnitList!.Add(new ProjectBusinessUnitViewModel() { ProjectId = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveProjectBusinessUnit()
	{
		ModelState.Clear();
		Project.ProjectBusinessUnitList = Project!.ProjectBusinessUnitList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}

	private IActionResult AddLevel()
	{
		ModelState.Clear();
		if (Project!.LevelList == null) { Project!.LevelList = new List<LevelViewModel>(); }
		Project!.LevelList!.Add(new LevelViewModel() { ProjectId = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveLevel()
	{
		ModelState.Clear();
		Project.LevelList = Project!.LevelList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}
	
}
