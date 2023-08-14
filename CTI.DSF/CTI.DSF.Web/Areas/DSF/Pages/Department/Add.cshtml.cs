using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Department;

[Authorize(Policy = Permission.Department.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public DepartmentViewModel Department { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddDepartmentCommand>(Department)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddSection")
		{
			return AddSection();
		}
		if (AsyncAction == "RemoveSection")
		{
			return RemoveSection();
		}
		
		
        return Partial("_InputFieldsPartial", Department);
    }
	
	private IActionResult AddSection()
	{
		ModelState.Clear();
		if (Department!.SectionList == null) { Department!.SectionList = new List<SectionViewModel>(); }
		Department!.SectionList!.Add(new SectionViewModel() { DepartmentCode = Department.Id });
		return Partial("_InputFieldsPartial", Department);
	}
	private IActionResult RemoveSection()
	{
		ModelState.Clear();
		Department.SectionList = Department!.SectionList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Department);
	}
	
}
