using CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectCategory;

[Authorize(Policy = Permission.ProjectCategory.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProjectCategoryViewModel ProjectCategory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectCategoryByIdQuery(id)), ProjectCategory);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProjectCategoryCommand>(ProjectCategory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ProjectCategory);
    }
	
}
