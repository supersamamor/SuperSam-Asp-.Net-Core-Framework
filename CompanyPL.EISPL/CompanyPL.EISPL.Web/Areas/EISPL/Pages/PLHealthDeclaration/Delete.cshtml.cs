using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLHealthDeclaration;

[Authorize(Policy = Permission.PLHealthDeclaration.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
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

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeletePLHealthDeclarationCommand { Id = PLHealthDeclaration.Id }), "Index");
    }
}
