using CTI.TenantSales.Application.Features.TenantSales.Theme.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Theme;

[Authorize(Policy = Permission.Theme.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ThemeViewModel Theme { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetThemeByIdQuery(id)), Theme);
    }
}
