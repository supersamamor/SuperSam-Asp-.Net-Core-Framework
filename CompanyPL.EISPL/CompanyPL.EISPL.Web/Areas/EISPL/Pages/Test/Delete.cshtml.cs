using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Queries;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.Test;

[Authorize(Policy = Permission.Test.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public TestViewModel Test { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTestByIdQuery(id)), Test);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteTestCommand { Id = Test.Id }), "Index");
    }
}
