using CTI.TenantSales.Core.TenantSales;

namespace CTI.TenantSales.Scheduler.Models
{
    public class POSSales
    {
        public string TenantCode { get; set; } = "";
        public string POSCode { get; set; } = "";
        public IList<SalesItem> SalesList { get; set; } = new List<SalesItem>();
    }
}
