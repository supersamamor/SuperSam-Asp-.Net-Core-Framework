namespace CTI.TenantSales.Web.Areas.Reports.Models
{
    public class SalesGrowthModel
    {        
        public int Year { get; set; } = DateTime.Today.Year;
        public int Month { get; set; } = DateTime.Today.Month - 1;
        public string? ProjectId { get; set; }
        public string? FilePath { get; set; }
    }    
}
