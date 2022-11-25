using CTI.ELMS.Application.Features.ELMS.Project.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Project;

[Authorize(Policy = Permission.Project.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProjectViewModel Project { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddProjectCommand>(Project)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddUserProjectAssignment")
		{
			return AddUserProjectAssignment();
		}
		if (AsyncAction == "RemoveUserProjectAssignment")
		{
			return RemoveUserProjectAssignment();
		}
        return Partial("_InputFieldsPartial", Project);
    }
	
	private IActionResult AddUserProjectAssignment()
	{
		ModelState.Clear();
		if (Project!.UserProjectAssignmentList == null) { Project!.UserProjectAssignmentList = new List<UserProjectAssignmentViewModel>(); }
		Project!.UserProjectAssignmentList!.Add(new UserProjectAssignmentViewModel() { ProjectID = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveUserProjectAssignment()
	{
		ModelState.Clear();
		Project.UserProjectAssignmentList = Project!.UserProjectAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}
}
