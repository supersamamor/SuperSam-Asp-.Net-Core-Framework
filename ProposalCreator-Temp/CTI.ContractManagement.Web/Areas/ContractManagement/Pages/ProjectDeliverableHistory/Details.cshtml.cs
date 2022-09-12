using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectDeliverableHistory;

[Authorize(Policy = Permission.ProjectDeliverableHistory.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
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
}
