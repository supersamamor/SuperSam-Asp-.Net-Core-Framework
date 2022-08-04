using CTI.TenantSales.Web.Areas.Sales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.TenantSales.Web.Areas.Sales.Pages.Main
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public TenantSalesModel Input { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
