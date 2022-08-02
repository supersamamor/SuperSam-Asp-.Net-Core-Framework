using CTI.Common.Core.Base.Models;
using CTI.TenantSales.Core.Constants;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantPOSSalesState : BaseEntity
{
    public int SalesType { get; set; } = Convert.ToInt32(SalesTypeEnum.Daily);
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
    public string? ValidationRemarks { get; set; }
    public decimal AutocalculatedNewAccumulatedTotal { get; private set; }
    public decimal AutocalculatedOldAccumulatedTotal { get; private set; }  
    public TenantPOSState? TenantPOS { get; init; }
    #region Derived Property
    #endregion
    public decimal AutoCalculatedTotalNetSales(decimal taxRate)
    {
        return taxRate == 0 ? 0 : Math.Round((this.TaxableSalesAmount - this.TotalServiceCharge) / taxRate, 2) + this.NonTaxableSalesAmount;
    }
    public void UpdateFrom(TenantPOSSalesState sales)
    {
        SalesType = sales.SalesType;
        HourCode = sales.HourCode;
        SalesCategory = sales.SalesCategory;
        SalesDate = sales.SalesDate;
        IsAutoCompute = sales.IsAutoCompute;
        SalesAmount = sales.SalesAmount;
        OldAccumulatedTotal = sales.OldAccumulatedTotal;
        NewAccumulatedTotal = sales.NewAccumulatedTotal;
        TaxableSalesAmount = sales.TaxableSalesAmount;
        NonTaxableSalesAmount = sales.NonTaxableSalesAmount;
        SeniorCitizenDiscount = sales.SeniorCitizenDiscount;
        PromoDiscount = sales.PromoDiscount;
        OtherDiscount = sales.OtherDiscount;
        RefundDiscount = sales.RefundDiscount;
        VoidAmount = sales.VoidAmount;
        AdjustmentAmount = sales.AdjustmentAmount;
        TotalServiceCharge = sales.TotalServiceCharge;
        TotalTax = sales.TotalTax;
        NoOfSalesTransactions = sales.NoOfSalesTransactions;
        NoOfTransactions = sales.NoOfTransactions;
        TotalNetSales = sales.TotalNetSales;
        ControlNumber = sales.ControlNumber;
        FileName = sales.FileName;
    }
    private void ValidateDaily(TenantPOSSalesState? previousSales, decimal taxRate, decimal salesAmountThreshold)
    {
        if (previousSales != null && previousSales.SalesDate.AddDays(1) != this.SalesDate)
        {
            throw new Exception("Invalid previous sales");
        }
        this.ValidationRemarks = null;
        this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Passed);
        if (this.IsAutoCompute == true && previousSales != null && previousSales.ValidationStatus == Convert.ToInt32(ValidationStatusEnum.Failed))
        {
            this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
            this.ValidationRemarks += "\\Previous day is failed.";
        }
        else if (this.IsAutoCompute == false)
        {
            if (previousSales == null)
            {
                this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
                this.ValidationRemarks += "\\There is no submitted sales from the previous day. If this is a beginning balance please tick the 'Manually Checked' checkbox";
            }
            else
            {
                if (previousSales.ValidationStatus == Convert.ToInt32(ValidationStatusEnum.Failed))
                {
                    this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
                    this.ValidationRemarks += "\\Previous day is failed.";
                }
                if (AutocalculatedOldAccumulatedTotal != previousSales.AutocalculatedNewAccumulatedTotal)
                {
                    this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
                    this.ValidationRemarks += "\\Old accumulated is not equal from the new accumulated of the previous day.";
                }
                if (this.ValidationStatus == Convert.ToInt32(ValidationStatusEnum.Passed))
                {
                    if (this.TotalNetSales == 0 && previousSales.AutocalculatedNewAccumulatedTotal != 0)
                    {
                        this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
                        this.ValidationRemarks += "\\Sales amount is zero. If this is fine please tick the 'Manually Checked' checkbox otherwise put a value on sales amount.";
                    }
                }
            }
            if (Math.Abs(this.TotalNetSales - this.AutoCalculatedTotalNetSales(taxRate)) > salesAmountThreshold)
            {
                this.ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed);
                this.ValidationRemarks += "\\Total net sales is not equal based on AutoCalculated.";
            }
        }
    }

    private void UpdateAutoCalculatedTotalAccumulatedSales(TenantPOSSalesState? prevSales, decimal taxRate)
    {
        if (prevSales != null)
        {
            if (prevSales.SalesDate.AddDays(1) != this.SalesDate)
            {
                throw new Exception("Invalid previous sales");
            }
            this.AutocalculatedOldAccumulatedTotal = prevSales.AutocalculatedNewAccumulatedTotal;
            this.AutocalculatedNewAccumulatedTotal = prevSales.AutocalculatedNewAccumulatedTotal + this.AutoCalculatedTotalNetSales(taxRate);
        }
        else if (prevSales == null)
        {
            this.AutocalculatedOldAccumulatedTotal = this.OldAccumulatedTotal;
            this.AutocalculatedNewAccumulatedTotal = this.OldAccumulatedTotal + this.AutoCalculatedTotalNetSales(taxRate);
        }
    }
    public void ProcessPreviousDaySales(TenantPOSSalesState? prevSales, decimal taxRate, decimal salesAmountThreshold)
    {
        this.ValidateDaily(prevSales, taxRate, salesAmountThreshold);
        this.UpdateAutoCalculatedTotalAccumulatedSales(prevSales, taxRate);
    }
    public void SetEntity(string? entity)
    {
        this.Entity = entity;
    }
}
