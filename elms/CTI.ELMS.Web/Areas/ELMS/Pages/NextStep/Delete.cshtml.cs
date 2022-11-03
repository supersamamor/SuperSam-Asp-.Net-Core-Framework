using CTI.ELMS.Application.Features.ELMS.NextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.NextStep.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.NextStep;

[Authorize(Policy = Permission.NextStep.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public NextStepViewModel NextStep { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetNextStepByIdQuery(id)), NextStep);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteNextStepCommand { Id = NextStep.Id }), "Index");
    }
}
