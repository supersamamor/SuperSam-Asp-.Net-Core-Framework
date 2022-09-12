using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectPackageHistory;

[Authorize(Policy = Permission.ProjectPackageHistory.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ProjectPackageHistoryViewModel ProjectPackageHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectPackageHistoryByIdQuery(id)), ProjectPackageHistory);
    }
}
