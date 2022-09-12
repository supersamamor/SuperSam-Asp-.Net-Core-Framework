using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectDeliverable;

[Authorize(Policy = Permission.ProjectDeliverable.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ProjectDeliverableViewModel ProjectDeliverable { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectDeliverableByIdQuery(id)), ProjectDeliverable);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteProjectDeliverableCommand { Id = ProjectDeliverable.Id }), "Index");
    }
}
