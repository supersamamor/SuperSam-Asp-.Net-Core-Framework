using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskList;

[Authorize(Policy = Permission.TaskList.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TaskListViewModel TaskList { get; set; } = new();
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
        if (TaskList.TaskClassification == Core.Constants.TaskClassifications.Recurring && TaskList.TaskDueDay == null)
        {
            NotyfService.Error(Localizer["Task due day is required."]);
            return Page();
        }
        if (TaskList.TaskClassification == Core.Constants.TaskClassifications.Adhoc && TaskList.TargetDueDate == null)
        {
            NotyfService.Error(Localizer["Target Due Date is required."]);
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTaskListCommand>(TaskList)), "Details", true);
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
		TaskList!.ChildTaskList!.Add(new TaskListViewModel() { ParentTaskId = TaskList.Id, IsMilestone = true });
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult RemoveChildTask()
	{
		ModelState.Clear();
		TaskList.ChildTaskList = TaskList!.ChildTaskList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskList);
	}
}
