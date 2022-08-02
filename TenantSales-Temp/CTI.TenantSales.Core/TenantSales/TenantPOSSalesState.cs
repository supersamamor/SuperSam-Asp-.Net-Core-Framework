using CTI.Common.Core.Base.Models;


namespace CTI.TenantSales.Core.TenantSales;

public record TenantPOSSalesState : BaseEntity
{
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
	public string? ValidationRemarks { get; set; }
	public decimal AutocalculatedNewAccumulatedTotal { get; init; }
	public decimal AutocalculatedOldAccumulatedTotal { get; init; }	
	public TenantPOSState? TenantPOS { get; init; }
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
}
