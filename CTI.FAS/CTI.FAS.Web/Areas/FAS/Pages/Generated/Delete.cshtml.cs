using CTI.FAS.Application.Features.FAS.Generated.Commands;
using CTI.FAS.Application.Features.FAS.Generated.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Generated;

[Authorize(Policy = Permission.Generated.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public GeneratedViewModel Generated { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetGeneratedByIdQuery(id)), Generated);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteGeneratedCommand { Id = Generated.Id }), "Index");
    }
}
