using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.MilestoneStage;

[Authorize(Policy = Permission.MilestoneStage.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public MilestoneStageViewModel MilestoneStage { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetMilestoneStageByIdQuery(id)), MilestoneStage);
    }
}
