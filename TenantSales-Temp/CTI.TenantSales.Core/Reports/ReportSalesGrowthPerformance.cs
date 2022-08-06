using CTI.TenantSales.Core.TenantSales;
namespace CTI.TenantSales.Core.Reports
{
    public record ReportSalesGrowthPerformance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TenantId { get; set; } = "";
        public int Year { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public decimal JanCAMCAirConEffRent { get; set; }
        public decimal FebCAMCAirConEffRent { get; set; }
        public decimal MarCAMCAirConEffRent { get; set; }
        public decimal AprCAMCAirConEffRent { get; set; }
        public decimal MayCAMCAirConEffRent { get; set; }
        public decimal JunCAMCAirConEffRent { get; set; }
        public decimal JulCAMCAirConEffRent { get; set; }
        public decimal AugCAMCAirConEffRent { get; set; }
        public decimal SeptCAMCAirConEffRent { get; set; }
        public decimal OctCAMCAirConEffRent { get; set; }
        public decimal NovCAMCAirConEffRent { get; set; }
        public decimal DecCAMCAirConEffRent { get; set; }   
        #region Navigation Properties        
        public TenantState Tenant { get; set; } = new TenantState();        
        #endregion
    }
}
