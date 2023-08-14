using CTI.DSF.Application.Features.DSF.Company.Commands;
using CTI.DSF.Application.Features.DSF.Company.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Company;

[Authorize(Policy = Permission.Company.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public CompanyViewModel Company { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetCompanyByIdQuery(id)), Company);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditCompanyCommand>(Company)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddDepartment")
		{
			return AddDepartment();
		}
		if (AsyncAction == "RemoveDepartment")
		{
			return RemoveDepartment();
		}
		
		
        return Partial("_InputFieldsPartial", Company);
    }
	
	private IActionResult AddDepartment()
	{
		ModelState.Clear();
		if (Company!.DepartmentList == null) { Company!.DepartmentList = new List<DepartmentViewModel>(); }
		Company!.DepartmentList!.Add(new DepartmentViewModel() { CompanyCode = Company.Id });
		return Partial("_InputFieldsPartial", Company);
	}
	private IActionResult RemoveDepartment()
	{
		ModelState.Clear();
		Company.DepartmentList = Company!.DepartmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Company);
	}
	
}
