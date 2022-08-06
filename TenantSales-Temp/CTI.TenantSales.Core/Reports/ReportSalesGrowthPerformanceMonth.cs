using CTI.TenantSales.Core.TenantSales;

namespace CTI.TenantSales.Core.Reports
{
    public record ReportSalesGrowthPerformanceMonth
    {      
        public string Id { get; set; } = Guid.NewGuid().ToString();      
        public int Year { get; set; }
        public int Month { get; set; }     
        public decimal PrevYearYTD { get; set; }
        public decimal CurrentYearYTD { get; set; }
        public decimal PercentVariance { get; set; }
        public decimal PercRentAirConCAMCOverCurrentMonthSales { get; set; }
        public decimal PercRentAirConCAMCOverCurrentYTDSales { get; set; }
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
        public string TenantId { get; set; } = "";
        #region Navigation Properties   
        public TenantState Tenant { get; set; } = new TenantState();
        #endregion
    }
}
