using CNPlaceHolder.Common.Web.Utility.Extensions;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Pages.Applications;

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
