using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTaskListCommand>(TaskList)), "Details", true);
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
		if (AsyncAction == "AddTaskTag")
		{
			return AddTaskTag();
		}
		if (AsyncAction == "RemoveTaskTag")
		{
			return RemoveTaskTag();
		}
		
		
        return Partial("_InputFieldsPartial", TaskList);
    }
	
	private IActionResult AddTaskApprover()
	{
		ModelState.Clear();
		if (TaskList!.TaskApproverList == null) { TaskList!.TaskApproverList = new List<TaskApproverViewModel>(); }
		TaskList!.TaskApproverList!.Add(new TaskApproverViewModel() { TaskListId = TaskList.Id });
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult RemoveTaskApprover()
	{
		ModelState.Clear();
		TaskList.TaskApproverList = TaskList!.TaskApproverList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskList);
	}

	private IActionResult AddTaskTag()
	{
		ModelState.Clear();
		if (TaskList!.TaskTagList == null) { TaskList!.TaskTagList = new List<TaskTagViewModel>(); }
		TaskList!.TaskTagList!.Add(new TaskTagViewModel() { TaskListId = TaskList.Id });
		return Partial("_InputFieldsPartial", TaskList);
	}
	private IActionResult RemoveTaskTag()
	{
		ModelState.Clear();
		TaskList.TaskTagList = TaskList!.TaskTagList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskList);
	}
	
}
