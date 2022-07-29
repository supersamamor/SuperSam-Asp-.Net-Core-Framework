using CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.ProjectBusinessUnit;

[Authorize(Policy = Permission.ProjectBusinessUnit.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ProjectBusinessUnitViewModel ProjectBusinessUnit { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectBusinessUnitByIdQuery(id)), ProjectBusinessUnit);
    }
}
