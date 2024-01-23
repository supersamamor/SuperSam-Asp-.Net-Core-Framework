using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.HealthDeclaration;

[Authorize(Policy = Permission.HealthDeclaration.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public HealthDeclarationViewModel HealthDeclaration { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddHealthDeclarationCommand>(HealthDeclaration)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", HealthDeclaration);
    }
	
}
