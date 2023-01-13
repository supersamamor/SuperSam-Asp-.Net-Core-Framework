using CTI.LocationApi.Application.Features.LocationApi.Approval.Queries;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.ApproverSetup;

[Authorize(Policy = Permission.ApproverSetup.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ApproverSetupViewModel ApproverSetup { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetApproverSetupByIdQuery(id)), ApproverSetup);
    }
}
