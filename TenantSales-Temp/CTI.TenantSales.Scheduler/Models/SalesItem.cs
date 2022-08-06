using CTI.TenantSales.Core.TenantSales;

namespace CTI.TenantSales.Scheduler.Models
{
    public record SalesItem 
    {
        public string? Id { get; set; }
        public int SalesType { get; set; }
        public int HourCode { get; set; }
        public string? SalesCategory { get; set; }
        public DateTime SalesDate { get; set; }
        public bool IsAutoCompute { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal OldAccumulatedTotal { get; set; }
        public decimal NewAccumulatedTotal { get; set; }
        public decimal TaxableSalesAmount { get; set; }
        public decimal NonTaxableSalesAmount { get; set; }
        public decimal SeniorCitizenDiscount { get; set; }
        public decimal PromoDiscount { get; set; }
        public decimal OtherDiscount { get; set; }
        public decimal RefundDiscount { get; set; }
        public decimal VoidAmount { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NoOfSalesTransactions { get; set; }
        public decimal NoOfTransactions { get; set; }
        public decimal TotalNetSales { get; set; }
        public int ControlNumber { get; set; }
        public string? FileName { get; set; }
        public string TenantPOSId { get; set; } = "";
        public int ValidationStatus { get; set; } 
        public string ValidationRemarks { get; set; } = "";
    }
}
