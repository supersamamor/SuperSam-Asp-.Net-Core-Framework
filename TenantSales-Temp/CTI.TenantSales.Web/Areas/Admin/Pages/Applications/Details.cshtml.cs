using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Areas.Admin.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.View)]
public class DetailsModel : BasePageModel<AddModel>
{
    public ApplicationViewModel Application { get; set; } = new();

    public IActionResult OnGet()
    {
        Application = TempData.Get<ApplicationViewModel>("Application") ?? new();
        return Page();
    }
}
