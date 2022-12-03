using CTI.FAS.Application.Features.FAS.Tenant.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Tenant;

[Authorize(Policy = Permission.Tenant.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public TenantViewModel Tenant { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTenantByIdQuery(id)), Tenant);
    }
}
