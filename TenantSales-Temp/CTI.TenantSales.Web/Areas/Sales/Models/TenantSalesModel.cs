namespace CTI.TenantSales.Web.Areas.Sales.Models
{
    public class TenantSalesModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? ProjectId { get; set; }
        public string? LevelId { get; set; }
        public string? TenantId { get; set; }
        public string? SalesCategoryCode { get; set; }
        public string? FilePath { get; set; }
    }
}
