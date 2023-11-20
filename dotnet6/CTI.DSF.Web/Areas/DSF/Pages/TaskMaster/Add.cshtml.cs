using CTI.DSF.Application.Features.DSF.TaskMaster.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskMaster;

[Authorize(Policy = Permission.TaskMaster.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TaskMasterViewModel TaskMaster { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTaskMasterCommand>(TaskMaster)), "Details", true);
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
		if (AsyncAction == "AddTaskTag")
		{
			return AddTaskTag();
		}
		if (AsyncAction == "RemoveTaskTag")
		{
			return RemoveTaskTag();
		}
		
		
        return Partial("_InputFieldsPartial", TaskMaster);
    }
	
	private IActionResult AddTaskCompanyAssignment()
	{
		ModelState.Clear();
		if (TaskMaster!.TaskCompanyAssignmentList == null) { TaskMaster!.TaskCompanyAssignmentList = new List<TaskCompanyAssignmentViewModel>(); }
		TaskMaster!.TaskCompanyAssignmentList!.Add(new TaskCompanyAssignmentViewModel() { TaskMasterId = TaskMaster.Id });
		return Partial("_InputFieldsPartial", TaskMaster);
	}
	private IActionResult RemoveTaskCompanyAssignment()
	{
		ModelState.Clear();
		TaskMaster.TaskCompanyAssignmentList = TaskMaster!.TaskCompanyAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskMaster);
	}

	private IActionResult AddTaskTag()
	{
		ModelState.Clear();
		if (TaskMaster!.TaskTagList == null) { TaskMaster!.TaskTagList = new List<TaskTagViewModel>(); }
		TaskMaster!.TaskTagList!.Add(new TaskTagViewModel() { TaskMasterId = TaskMaster.Id });
		return Partial("_InputFieldsPartial", TaskMaster);
	}
	private IActionResult RemoveTaskTag()
	{
		ModelState.Clear();
		TaskMaster.TaskTagList = TaskMaster!.TaskTagList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", TaskMaster);
	}
	
}
