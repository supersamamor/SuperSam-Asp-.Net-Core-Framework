using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.LeadTouchPoint;

[Authorize(Policy = Permission.LeadTouchPoint.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public LeadTouchPointViewModel LeadTouchPoint { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetLeadTouchPointByIdQuery(id)), LeadTouchPoint);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteLeadTouchPointCommand { Id = LeadTouchPoint.Id }), "Index");
    }
}
