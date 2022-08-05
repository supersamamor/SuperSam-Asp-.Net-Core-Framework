using CTI.TenantSales.Web.Areas.Sales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CTI.TenantSales.Web.Areas.Sales.Pages.Main
{
    [Authorize(Policy = Permission.Sales.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public TenantSalesModel Input { get; set; } = new();
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPostChangeFormValue()
        {
            ModelState.Clear();
            return Partial("_InputFieldsPartial", Input);
        }
    }
}
