using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLEmployee;

[Authorize(Policy = Permission.PLEmployee.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public PLEmployeeViewModel PLEmployee { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		this.PLEmployee.PLHealthDeclarationList = new List<PLHealthDeclarationViewModel>() { new PLHealthDeclarationViewModel() { PLEmployeeId = this.PLEmployee.Id} };
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddPLEmployeeCommand>(PLEmployee)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddPLContactInformation")
		{
			return AddPLContactInformation();
		}
		if (AsyncAction == "RemovePLContactInformation")
		{
			return RemovePLContactInformation();
		}
		
		
        return Partial("_InputFieldsPartial", PLEmployee);
    }
	
	private IActionResult AddPLContactInformation()
	{
		ModelState.Clear();
		if (PLEmployee!.PLContactInformationList == null) { PLEmployee!.PLContactInformationList = new List<PLContactInformationViewModel>(); }
		PLEmployee!.PLContactInformationList!.Add(new PLContactInformationViewModel() { PLEmployeeId = PLEmployee.Id });
		return Partial("_InputFieldsPartial", PLEmployee);
	}
	private IActionResult RemovePLContactInformation()
	{
		ModelState.Clear();
		PLEmployee.PLContactInformationList = PLEmployee!.PLContactInformationList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", PLEmployee);
	}
	
}
