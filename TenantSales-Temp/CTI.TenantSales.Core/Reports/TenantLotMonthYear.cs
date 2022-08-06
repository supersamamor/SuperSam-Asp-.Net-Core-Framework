using CTI.TenantSales.Core.TenantSales;


namespace CTI.TenantSales.Core.Reports
{
    public record TenantLotMonthYear
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();     
        public int Year { get; set; }
        public int Month { get; set; }
        public string LotNo { get; set; } = "";
        public decimal Area { get; set; }
        public string TenantId { get; set; } = "";
        #region Navigation Properties   
        public TenantState Tenant { get; set; } = new TenantState();
        #endregion
    }
}
