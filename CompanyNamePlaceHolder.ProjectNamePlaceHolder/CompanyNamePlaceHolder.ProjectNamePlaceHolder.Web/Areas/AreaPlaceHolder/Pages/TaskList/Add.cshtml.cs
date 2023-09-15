using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.TaskList;

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
		if (AsyncAction == "AddAssignment")
		{
			return AddAssignment();
		}
		if (AsyncAction == "RemoveAssignment")
		{
			return RemoveAssignment();
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
	
}
