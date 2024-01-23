using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.HealthDeclaration;

[Authorize(Policy = Permission.HealthDeclaration.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
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
}
