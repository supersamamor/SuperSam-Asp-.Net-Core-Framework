namespace CTI.TenantSales.ExcelProcessor.Models
{
    public class TenantDailySales
    {
        public TenantDailySales()
        {
            if (this.TenantPOSList != null)
            {
                foreach (var posItem in this.TenantPOSList)
                {
                    DailySalesSummary.AddRange(posItem.TenantPOSSalesList);
                }
            }
        }
        private List<DailySales> DailySalesSummary
        {
            get
            {
                List<DailySales> dailySalesSummary = new();
                if (this.TenantPOSList != null)
                {
                    foreach (var posItem in this.TenantPOSList)
                    {
                        dailySalesSummary.AddRange(posItem.TenantPOSSalesList);
                    }
                }
                return dailySalesSummary;
            }
        }
        public string TenantCode { get; set; } = "";
        public string TenantName { get; set; } = "";
        public string ProjectName { get; set; } = "";
        public IList<TenantPOSDailySales> TenantPOSList { get; set; } = new List<TenantPOSDailySales>();
        public string TaxableSalesAmount
        {
            get
            {
                var taxableSalesAmount = this.DailySalesSummary.Sum(l => l.TaxableSalesAmount);
                return taxableSalesAmount == 0 ? "-" : taxableSalesAmount.ToString("##,##.00");
            }
        }
        public string NonTaxableSalesAmount
        {
            get
            {
                var nonTaxableSalesAmount = this.DailySalesSummary.Sum(l => l.NonTaxableSalesAmount);
                return nonTaxableSalesAmount == 0 ? "-" : nonTaxableSalesAmount.ToString("##,##.00");
            }
        }
        public string SeniorCitizenDiscount
        {
            get
            {
                var seniorCitizenDiscount = this.DailySalesSummary.Sum(l => l.SeniorCitizenDiscount);
                return seniorCitizenDiscount == 0 ? "-" : seniorCitizenDiscount.ToString("##,##.00");
            }
        }
        public string PromoDiscount
        {
            get
            {
                var promoDiscount = this.DailySalesSummary.Sum(l => l.PromoDiscount);
                return promoDiscount == 0 ? "-" : promoDiscount.ToString("##,##.00");
            }
        }
        public string OtherDiscount
        {
            get
            {
                var otherDiscount = this.DailySalesSummary.Sum(l => l.OtherDiscount);
                return otherDiscount == 0 ? "-" : otherDiscount.ToString("##,##.00");
            }
        }
        public string RefundDiscount
        {
            get
            {
                var refundDiscount = this.DailySalesSummary.Sum(l => l.RefundDiscount);
                return refundDiscount == 0 ? "-" : refundDiscount.ToString("##,##.00");
            }
        }
        public string VoidAmount
        {
            get
            {
                var voidAmount = this.DailySalesSummary.Sum(l => l.VoidAmount);
                return voidAmount == 0 ? "-" : voidAmount.ToString("##,##.00");
            }
        }
        public string AdjustmentAmount
        {
            get
            {
                var adjustmentAmount = this.DailySalesSummary.Sum(l => l.AdjustmentAmount);
                return adjustmentAmount == 0 ? "-" : adjustmentAmount.ToString("##,##.00");
            }
        }
        public string TotalServiceCharge
        {
            get
            {
                var totalServiceCharge = this.DailySalesSummary.Sum(l => l.TotalServiceCharge);
                return totalServiceCharge == 0 ? "-" : totalServiceCharge.ToString("##,##.00");
            }
        }
        public string TotalTax
        {
            get
            {
                var totalTax = this.DailySalesSummary.Sum(l => l.TotalTax);
                return totalTax == 0 ? "-" : totalTax.ToString("##,##.00");
            }
        }
        public string TotalNetSales
        {
            get
            {
                var totalNetSales = this.DailySalesSummary.Sum(l => l.TotalNetSales);
                return totalNetSales == 0 ? "-" : totalNetSales.ToString("##,##.00");
            }
        }
        public string NoOfTransactions
        {
            get
            {
                return this.DailySalesSummary.Sum(l => l.NoOfTransactions).ToString("0.##");
            }
        }
        public string NoOfSalesTransactions
        {
            get
            {
                return this.DailySalesSummary.Sum(l => l.NoOfSalesTransactions).ToString("0.##");
            }
        }
        public bool ShowSalesSummary
        {
            get
            {
                return TenantPOSList.Where(l => l.ShowSales == true).ToList().Count > 0;
            }
        }
    }
    public class DailySales
    {
        public string SalesCategory { get; set; } = "";
        public DateTime SalesDate { get; set; }
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
        public int ValidationStatus { get; set; }
        public string ValidationRemarks { get; set; } = "";
        #region ReadOnlyProperties    
        public string SalesDateFormatted
        {
            get
            {
                return this.SalesDate.ToString("MM/dd/yyyy");
            }
        }
        public string OldAccumulatedTotalFormatted
        {
            get
            {
                return this.OldAccumulatedTotal == 0 ? "-" : this.OldAccumulatedTotal.ToString("##,##.00");
            }
        }
        public string NewAccumulatedTotalFormatted
        {
            get
            {
                return this.NewAccumulatedTotal == 0 ? "-" : this.NewAccumulatedTotal.ToString("##,##.00");
            }
        }
        public string TaxableSalesAmountFormatted
        {
            get
            {
                return this.TaxableSalesAmount == 0 ? "-" : this.TaxableSalesAmount.ToString("##,##.00");
            }
        }
        public string NonTaxableSalesAmountFormatted
        {
            get
            {
                return this.NonTaxableSalesAmount == 0 ? "-" : this.NonTaxableSalesAmount.ToString("##,##.00");
            }
        }
        public string SeniorCitizenDiscountFormatted
        {
            get
            {
                return this.SeniorCitizenDiscount == 0 ? "-" : this.SeniorCitizenDiscount.ToString("##,##.00");
            }
        }
        public string PromoDiscountFormatted
        {
            get
            {
                return this.PromoDiscount == 0 ? "-" : this.PromoDiscount.ToString("##,##.00");
            }
        }
        public string OtherDiscountFormatted
        {
            get
            {
                return this.OtherDiscount == 0 ? "-" : this.OtherDiscount.ToString("##,##.00");
            }
        }
        public string RefundDiscountFormatted
        {
            get
            {
                return this.RefundDiscount == 0 ? "-" : this.RefundDiscount.ToString("##,##.00");
            }
        }
        public string VoidAmountFormatted
        {
            get
            {
                return this.VoidAmount == 0 ? "-" : this.VoidAmount.ToString("##,##.00");
            }
        }
        public string AdjustmentAmountFormatted
        {
            get
            {
                return this.AdjustmentAmount == 0 ? "-" : this.AdjustmentAmount.ToString("##,##.00");
            }
        }
        public string TotalServiceChargeFormatted
        {
            get
            {
                return this.TotalServiceCharge == 0 ? "-" : this.TotalServiceCharge.ToString("##,##.00");
            }
        }
        public string TotalTaxFormatted
        {
            get
            {
                return this.TotalTax == 0 ? "-" : this.TotalTax.ToString("##,##.00");
            }
        }
        public string TotalNetSalesFormatted
        {
            get
            {
                return this.TotalNetSales == 0 ? "-" : this.TotalNetSales.ToString("##,##.00");
            }
        }
        public string NoOfSalesTransactionsFormatted
        {
            get
            {
                return this.NoOfSalesTransactions.ToString("0.##");
            }
        }
        public string NoOfTransactionsFormatted
        {
            get
            {
                return this.NoOfTransactions.ToString("0.##");
            }
        }

        public int DayNumber { get; set; }
        public void SetDayNumber(DateTime dateFrom)
        {
            this.DayNumber = (int)((this.SalesDate - dateFrom).TotalDays) + 1;
        }
        #endregion
    }
    public class TenantPOSDailySales
    {
        public bool ShowSales
        {
            get
            {
                return TenantPOSSalesList.Sum(l => l.TotalNetSales) != 0;
            }
        }
        public string Code { get; set; } = "";
        public string TaxableSalesAmount
        {
            get
            {
                var taxableSalesAmount = this.TenantPOSSalesList.Sum(l => l.TaxableSalesAmount);
                return taxableSalesAmount == 0 ? "-" : taxableSalesAmount.ToString("##,##.00");
            }
        }
        public string NonTaxableSalesAmount
        {
            get
            {
                var nonTaxableSalesAmount = this.TenantPOSSalesList.Sum(l => l.NonTaxableSalesAmount);
                return nonTaxableSalesAmount == 0 ? "-" : nonTaxableSalesAmount.ToString("##,##.00");
            }
        }
        public string SeniorCitizenDiscount
        {
            get
            {
                var seniorCitizenDiscount = this.TenantPOSSalesList.Sum(l => l.SeniorCitizenDiscount);
                return seniorCitizenDiscount == 0 ? "-" : seniorCitizenDiscount.ToString("##,##.00");
            }
        }
        public string PromoDiscount
        {
            get
            {
                var promoDiscount = this.TenantPOSSalesList.Sum(l => l.PromoDiscount);
                return promoDiscount == 0 ? "-" : promoDiscount.ToString("##,##.00");
            }
        }
        public string OtherDiscount
        {
            get
            {
                var otherDiscount = this.TenantPOSSalesList.Sum(l => l.OtherDiscount);
                return otherDiscount == 0 ? "-" : otherDiscount.ToString("##,##.00");
            }
        }
        public string RefundDiscount
        {
            get
            {
                var refundDiscount = this.TenantPOSSalesList.Sum(l => l.RefundDiscount);
                return refundDiscount == 0 ? "-" : refundDiscount.ToString("##,##.00");
            }
        }
        public string VoidAmount
        {
            get
            {
                var voidAmount = this.TenantPOSSalesList.Sum(l => l.VoidAmount);
                return voidAmount == 0 ? "-" : voidAmount.ToString("##,##.00");
            }
        }
        public string AdjustmentAmount
        {
            get
            {
                var adjustmentAmount = this.TenantPOSSalesList.Sum(l => l.AdjustmentAmount);
                return adjustmentAmount == 0 ? "-" : adjustmentAmount.ToString("##,##.00");
            }
        }
        public string TotalServiceCharge
        {
            get
            {
                var totalServiceCharge = this.TenantPOSSalesList.Sum(l => l.TotalServiceCharge);
                return totalServiceCharge == 0 ? "-" : totalServiceCharge.ToString("##,##.00");
            }
        }
        public string TotalTax
        {
            get
            {
                var totalTax = this.TenantPOSSalesList.Sum(l => l.TotalTax);
                return totalTax == 0 ? "-" : totalTax.ToString("##,##.00");
            }
        }
        public string TotalNetSales
        {
            get
            {
                var totalNetSales = this.TenantPOSSalesList.Sum(l => l.TotalNetSales);
                return totalNetSales == 0 ? "-" : totalNetSales.ToString("##,##.00");
            }
        }
        public string NoOfTransactions
        {
            get
            {
                return this.TenantPOSSalesList.Sum(l => l.NoOfTransactions).ToString("0.##");
            }
        }
        public string NoOfSalesTransactions
        {
            get
            {
                return this.TenantPOSSalesList.Sum(l => l.NoOfSalesTransactions).ToString("0.##");
            }
        }
        public IList<DailySales> TenantPOSSalesList { get; set; } = new List<DailySales>();
    }
}
