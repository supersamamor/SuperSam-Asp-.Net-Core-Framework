using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLHealthDeclaration;

[Authorize(Policy = Permission.PLHealthDeclaration.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public PLHealthDeclarationViewModel PLHealthDeclaration { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetPLHealthDeclarationByIdQuery(id)), PLHealthDeclaration);
    }
}
