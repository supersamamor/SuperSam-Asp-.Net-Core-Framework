using CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.BusinessUnit;

[Authorize(Policy = Permission.BusinessUnit.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public BusinessUnitViewModel BusinessUnit { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddBusinessUnitCommand>(BusinessUnit)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectBusinessUnit")
		{
			return AddProjectBusinessUnit();
		}
		if (AsyncAction == "RemoveProjectBusinessUnit")
		{
			return RemoveProjectBusinessUnit();
		}
		
		
        return Partial("_InputFieldsPartial", BusinessUnit);
    }
	
	private IActionResult AddProjectBusinessUnit()
	{
		ModelState.Clear();
		if (BusinessUnit!.ProjectBusinessUnitList == null) { BusinessUnit!.ProjectBusinessUnitList = new List<ProjectBusinessUnitViewModel>(); }
		BusinessUnit!.ProjectBusinessUnitList!.Add(new ProjectBusinessUnitViewModel() { BusinessUnitId = BusinessUnit.Id });
		return Partial("_InputFieldsPartial", BusinessUnit);
	}
	private IActionResult RemoveProjectBusinessUnit()
	{
		ModelState.Clear();
		BusinessUnit.ProjectBusinessUnitList = BusinessUnit!.ProjectBusinessUnitList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", BusinessUnit);
	}
	
}
