using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.TenantSales.Web.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        return Redirect("Sales/Main/Index");
    }
}
