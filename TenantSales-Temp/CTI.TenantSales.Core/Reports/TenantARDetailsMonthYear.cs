using CTI.TenantSales.Core.TenantSales;


namespace CTI.TenantSales.Core.Reports
{
    public record TenantARDetailsMonthYear
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal CAMCRate { get; set; }
        public decimal AirConRate { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal MBaseAmount { get; set; }
        public decimal EffectiveRent { get; set; }
        public string TenantId { get; set; } = "";
        #region Navigation Properties   
        public TenantState Tenant { get; set; } = new TenantState();
        #endregion
    }
}
