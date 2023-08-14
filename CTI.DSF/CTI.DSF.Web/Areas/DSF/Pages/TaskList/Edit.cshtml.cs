using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.TaskList.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskList;

[Authorize(Policy = Permission.TaskList.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TaskListViewModel TaskList { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTaskListByIdQuery(id)), TaskList);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (TaskList.TaskClassification == Core.Constants.TaskClassifications.Recurring && TaskList.TaskDueDay == null)
        {
            NotyfService.Error(Localizer["Task due day is required."]);
        }
        if (TaskList.TaskClassification == Core.Constants.TaskClassifications.Adhoc && TaskList.TargetDueDate == null)
        {
            NotyfService.Error(Localizer["Target Due Date is required."]);
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTaskListCommand>(TaskList)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddAssignment")
		{
			return AddAssignment();
		}
		if (AsyncAction == "RemoveAssignment")
		{
			return RemoveAssignment();
		}
		if (AsyncAction == "AddChildTask")
		{
			return AddChildTask();
		}
		if (AsyncAction == "RemoveChildTask")
		{
			return RemoveChildTask();
		}

		return Partial("_InputFieldsPartial", TaskList);
    }
	
	private IActionResult AddAssignment()
	{
		ModelState.Clear();
		if (TaskList!.AssignmentList == null) { TaskList!.AssignmentList = new List<AssignmentViewModel>(); }
		TaskList!.AssignmentList!.Add(new AssignmentViewModel() { TaskListCode = TaskList.Id });
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult RemoveAssignment()
	{
		ModelState.Clear();
		TaskList.AssignmentList = TaskList!.AssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult AddChildTask()
	{
		ModelState.Clear();
		if (TaskList!.ChildTaskList == null) { TaskList!.ChildTaskList = new List<TaskListViewModel>(); }
		TaskList!.ChildTaskList!.Add(new TaskListViewModel() { ParentTaskId = TaskList.Id });
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult RemoveChildTask()
	{
		ModelState.Clear();
		TaskList.ChildTaskList = TaskList!.ChildTaskList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskList);
	}
}
