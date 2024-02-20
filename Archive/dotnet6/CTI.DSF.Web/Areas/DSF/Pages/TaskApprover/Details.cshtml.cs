using CTI.DSF.Application.Features.DSF.TaskApprover.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskApprover;

[Authorize(Policy = Permission.TaskApprover.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public TaskApproverViewModel TaskApprover { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTaskApproverByIdQuery(id)), TaskApprover);
    }
}
