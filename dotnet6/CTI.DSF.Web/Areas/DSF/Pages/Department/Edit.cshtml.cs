using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Application.Features.DSF.Department.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Department;

[Authorize(Policy = Permission.Department.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public DepartmentViewModel Department { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetDepartmentByIdQuery(id)), Department);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditDepartmentCommand>(Department)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", Department);
    }
	
	private IActionResult AddTaskCompanyAssignment()
	{
		ModelState.Clear();
		if (Department!.TaskCompanyAssignmentList == null) { Department!.TaskCompanyAssignmentList = new List<TaskCompanyAssignmentViewModel>(); }
		Department!.TaskCompanyAssignmentList!.Add(new TaskCompanyAssignmentViewModel() { DepartmentId = Department.Id });
		return Partial("_InputFieldsPartial", Department);
	}
	private IActionResult RemoveTaskCompanyAssignment()
	{
		ModelState.Clear();
		Department.TaskCompanyAssignmentList = Department!.TaskCompanyAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Department);
	}
	
}
