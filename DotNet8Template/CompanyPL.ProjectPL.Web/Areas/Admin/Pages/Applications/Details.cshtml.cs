using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Pages.Applications;

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
