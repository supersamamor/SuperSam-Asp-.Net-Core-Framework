using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.Employee;

[Authorize(Policy = Permission.Employee.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public EmployeeViewModel Employee { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		this.Employee.HealthDeclarationList = new List<HealthDeclarationViewModel>() { new HealthDeclarationViewModel() { EmployeeId = this.Employee.Id} };
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddEmployeeCommand>(Employee)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddContactInformation")
		{
			return AddContactInformation();
		}
		if (AsyncAction == "RemoveContactInformation")
		{
			return RemoveContactInformation();
		}
		
		
        return Partial("_InputFieldsPartial", Employee);
    }
	
	private IActionResult AddContactInformation()
	{
		ModelState.Clear();
		if (Employee!.ContactInformationList == null) { Employee!.ContactInformationList = new List<ContactInformationViewModel>(); }
		Employee!.ContactInformationList!.Add(new ContactInformationViewModel() { EmployeeId = Employee.Id });
		return Partial("_InputFieldsPartial", Employee);
	}
	private IActionResult RemoveContactInformation()
	{
		ModelState.Clear();
		Employee.ContactInformationList = Employee!.ContactInformationList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Employee);
	}
	
}
