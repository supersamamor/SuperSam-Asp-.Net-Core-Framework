using CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Commands;
using CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskCompanyAssignment;

[Authorize(Policy = Permission.TaskCompanyAssignment.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TaskCompanyAssignmentViewModel TaskCompanyAssignment { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTaskCompanyAssignmentByIdQuery(id)), TaskCompanyAssignment);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTaskCompanyAssignmentCommand>(TaskCompanyAssignment)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddTaskApprover")
		{
			return AddTaskApprover();
		}
		if (AsyncAction == "RemoveTaskApprover")
		{
			return RemoveTaskApprover();
		}
		
		
        return Partial("_InputFieldsPartial", TaskCompanyAssignment);
    }
	
	private IActionResult AddTaskApprover()
	{
		ModelState.Clear();
		if (TaskCompanyAssignment!.TaskApproverList == null) { TaskCompanyAssignment!.TaskApproverList = new List<TaskApproverViewModel>(); }
		TaskCompanyAssignment!.TaskApproverList!.Add(new TaskApproverViewModel() { TaskCompanyAssignmentId = TaskCompanyAssignment.Id });
		return Partial("_InputFieldsPartial", TaskCompanyAssignment);
	}
	private IActionResult RemoveTaskApprover()
	{
		ModelState.Clear();
		TaskCompanyAssignment.TaskApproverList = TaskCompanyAssignment!.TaskApproverList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskCompanyAssignment);
	}
	
}
