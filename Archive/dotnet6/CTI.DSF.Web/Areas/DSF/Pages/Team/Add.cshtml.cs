using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Team;

[Authorize(Policy = Permission.Team.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TeamViewModel Team { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTeamCommand>(Team)), "Details", true);
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
