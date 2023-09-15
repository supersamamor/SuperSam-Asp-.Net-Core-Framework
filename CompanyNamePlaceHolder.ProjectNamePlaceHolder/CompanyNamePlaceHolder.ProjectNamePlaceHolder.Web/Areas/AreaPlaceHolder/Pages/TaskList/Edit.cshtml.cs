using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.TaskList;

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
