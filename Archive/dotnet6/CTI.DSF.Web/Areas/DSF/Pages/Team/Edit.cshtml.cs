using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Application.Features.DSF.Team.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Team;

[Authorize(Policy = Permission.Team.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TeamViewModel Team { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTeamByIdQuery(id)), Team);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTeamCommand>(Team)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddTaskCompanyAssignment")
		{
			return AddTaskCompanyAssignment();
		}
		if (AsyncAction == "RemoveTaskCompanyAssignment")
		{
			return RemoveTaskCompanyAssignment();
		}
		
		
        return Partial("_InputFieldsPartial", Team);
    }
	
	private IActionResult AddTaskCompanyAssignment()
	{
		ModelState.Clear();
		if (Team!.TaskCompanyAssignmentList == null) { Team!.TaskCompanyAssignmentList = new List<TaskCompanyAssignmentViewModel>(); }
		Team!.TaskCompanyAssignmentList!.Add(new TaskCompanyAssignmentViewModel() { TeamId = Team.Id });
		return Partial("_InputFieldsPartial", Team);
	}
	private IActionResult RemoveTaskCompanyAssignment()
	{
		ModelState.Clear();
		Team.TaskCompanyAssignmentList = Team!.TaskCompanyAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Team);
	}
	
}
