using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectMilestone;

[Authorize(Policy = Permission.ProjectMilestone.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ProjectMilestoneViewModel ProjectMilestone { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectMilestoneByIdQuery(id)), ProjectMilestone);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteProjectMilestoneCommand { Id = ProjectMilestone.Id }), "Index");
    }
}
