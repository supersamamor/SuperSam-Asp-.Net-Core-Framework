using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace CompanyPL.ProjectPL.Web.Areas.Identity.Pages.Account;
[AllowAnonymous]
public class SiteNotYetAvailableModel : PageModel
{
    public IActionResult OnGet()  {
        return Page();
    }
}
