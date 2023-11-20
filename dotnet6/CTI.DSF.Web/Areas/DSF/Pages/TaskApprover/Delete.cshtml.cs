using CTI.DSF.Application.Features.DSF.TaskApprover.Commands;
using CTI.DSF.Application.Features.DSF.TaskApprover.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskApprover;

[Authorize(Policy = Permission.TaskApprover.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
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

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteTaskApproverCommand { Id = TaskApprover.Id }), "Index");
    }
}
