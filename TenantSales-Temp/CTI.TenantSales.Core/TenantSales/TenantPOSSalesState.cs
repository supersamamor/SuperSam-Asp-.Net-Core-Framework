using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantPOSSalesState : BaseEntity
{
	public int SalesType { get; init; }
	public int HourCode { get; init; }
	public string? SalesCategory { get; init; }
	public DateTime SalesDate { get; init; }
	public bool IsAutoCompute { get; init; }
	public decimal SalesAmount { get; init; }
	public decimal OldAccumulatedTotal { get; init; }
	public decimal NewAccumulatedTotal { get; init; }
	public decimal TaxableSalesAmount { get; init; }
	public decimal NonTaxableSalesAmount { get; init; }
	public decimal SeniorCitizenDiscount { get; init; }
	public decimal PromoDiscount { get; init; }
	public decimal OtherDiscount { get; init; }
	public decimal RefundDiscount { get; init; }
	public decimal VoidAmount { get; init; }
	public decimal AdjustmentAmount { get; init; }
	public decimal TotalServiceCharge { get; init; }
	public decimal TotalTax { get; init; }
	public decimal NoOfSalesTransactions { get; init; }
	public decimal NoOfTransactions { get; init; }
	public decimal TotalNetSales { get; init; }
	public int ControlNumber { get; init; }
	public string? FileName { get; init; }
	public string TenantPOSId { get; init; } = "";
	public int ValidationStatus { get; init; }
	public string? ValidationRemarks { get; init; }
	public decimal AutocalculatedNewAccumulatedTotal { get; init; }
	public decimal AutocalculatedOldAccumulatedTotal { get; init; }
	
	public TenantPOSState? TenantPOS { get; init; }
	
	
}
