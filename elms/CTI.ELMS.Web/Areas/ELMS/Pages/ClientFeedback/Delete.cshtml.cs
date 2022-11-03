using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;
using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.ClientFeedback;

[Authorize(Policy = Permission.ClientFeedback.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ClientFeedbackViewModel ClientFeedback { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetClientFeedbackByIdQuery(id)), ClientFeedback);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteClientFeedbackCommand { Id = ClientFeedback.Id }), "Index");
    }
}
