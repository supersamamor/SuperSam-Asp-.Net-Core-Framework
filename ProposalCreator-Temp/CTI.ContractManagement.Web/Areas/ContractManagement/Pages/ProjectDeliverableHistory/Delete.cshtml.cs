using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectDeliverableHistory;

[Authorize(Policy = Permission.ProjectDeliverableHistory.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ProjectDeliverableHistoryViewModel ProjectDeliverableHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectDeliverableHistoryByIdQuery(id)), ProjectDeliverableHistory);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteProjectDeliverableHistoryCommand { Id = ProjectDeliverableHistory.Id }), "Index");
    }
}
