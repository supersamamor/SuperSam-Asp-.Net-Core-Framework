using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectPackageAdditionalDeliverableHistory;

[Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ProjectPackageAdditionalDeliverableHistoryViewModel ProjectPackageAdditionalDeliverableHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectPackageAdditionalDeliverableHistoryByIdQuery(id)), ProjectPackageAdditionalDeliverableHistory);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteProjectPackageAdditionalDeliverableHistoryCommand { Id = ProjectPackageAdditionalDeliverableHistory.Id }), "Index");
    }
}
