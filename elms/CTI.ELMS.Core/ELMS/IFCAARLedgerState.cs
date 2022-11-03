using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record IFCAARLedgerState : BaseEntity
{
	public string TenantContractNo { get; init; } = "";
	public string DocumentNo { get; init; } = "";
	public DateTime? DocumentDate { get; init; }
	public string DocumentDescription { get; init; } = "";
	public string Mode { get; init; } = "";
	public string LedgerDescription { get; init; } = "";
	public decimal? TransactionWithHoldingTaxAmount { get; init; }
	public string TransactionType { get; init; } = "";
	public decimal? TransactionAmount { get; init; }
	public string LotNo { get; init; } = "";
	public int? LineNo { get; init; }
	public string TaxScheme { get; init; } = "";
	public decimal? TransactionTaxBaseAmount { get; init; }
	public decimal? TransactionTaxAmount { get; init; }
	public int? SequenceNo { get; init; }
	public string ReferenceNo { get; init; } = "";
	public string TransactionClass { get; init; } = "";
	public string GLAccount { get; init; } = "";
	public string? ProjectID { get; init; }
	public string TradeName { get; init; } = "";
	public string TransactionDesc { get; init; } = "";
	
	public ProjectState? Project { get; init; }
	
	
}
