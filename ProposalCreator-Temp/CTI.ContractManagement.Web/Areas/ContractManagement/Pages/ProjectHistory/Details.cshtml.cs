using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectHistory;

[Authorize(Policy = Permission.ProjectHistory.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ProjectHistoryViewModel ProjectHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectHistoryByIdQuery(id)), ProjectHistory);
    }
}
