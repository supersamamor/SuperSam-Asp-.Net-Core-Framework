using CTI.DSF.Application.Features.DSF.Company.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Company;

[Authorize(Policy = Permission.Company.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public CompanyViewModel Company { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddCompanyCommand>(Company)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", Company);
    }
	
	private IActionResult AddTaskCompanyAssignment()
	{
		ModelState.Clear();
		if (Company!.TaskCompanyAssignmentList == null) { Company!.TaskCompanyAssignmentList = new List<TaskCompanyAssignmentViewModel>(); }
		Company!.TaskCompanyAssignmentList!.Add(new TaskCompanyAssignmentViewModel() { CompanyId = Company.Id });
		return Partial("_InputFieldsPartial", Company);
	}
	private IActionResult RemoveTaskCompanyAssignment()
	{
		ModelState.Clear();
		Company.TaskCompanyAssignmentList = Company!.TaskCompanyAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Company);
	}
	
}
