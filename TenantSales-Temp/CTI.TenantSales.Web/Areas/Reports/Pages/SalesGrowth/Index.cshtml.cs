using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CTI.TenantSales.Web.Areas.Reports.Pages.SalesGrowth
{ 
    [Authorize(Policy = Permission.Reports.DailySales)]
    public class IndexModel : BasePageModel<IndexModel>
    {      
        public void OnGet()
        {
        }
    }
}
