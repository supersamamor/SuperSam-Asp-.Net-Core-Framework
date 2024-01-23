using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.HealthDeclaration;

[Authorize(Policy = Permission.HealthDeclaration.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public HealthDeclarationViewModel HealthDeclaration { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetHealthDeclarationByIdQuery(id)), HealthDeclaration);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteHealthDeclarationCommand { Id = HealthDeclaration.Id }), "Index");
    }
}
