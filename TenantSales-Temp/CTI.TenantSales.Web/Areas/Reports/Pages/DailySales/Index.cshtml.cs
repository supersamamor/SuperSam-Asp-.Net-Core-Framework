using CTI.TenantSales.Web.Areas.Reports.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.TenantSales.Web.Areas.Reports.Pages.DailySales
{
    [Authorize(Policy = Permission.Reports.DailySales)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public DailySalesModel Input { get; set; } = new();
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
