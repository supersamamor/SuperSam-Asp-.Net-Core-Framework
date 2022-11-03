using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record IFCAARLedgerViewModel : BaseViewModel
{	
	[Display(Name = "Tenant Contract No.")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantContractNo { get; init; } = "";
	[Display(Name = "Document No")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNo { get; init; } = "";
	[Display(Name = "Document Date")]
	public DateTime? DocumentDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Document Description")]
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentDescription { get; init; } = "";
	[Display(Name = "Mode")]
	[Required]
	[StringLength(10, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Mode { get; init; } = "";
	[Display(Name = "Ledger Description")]
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LedgerDescription { get; init; } = "";
	[Display(Name = "Transaction Withholding Tax Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionWithHoldingTaxAmount { get; init; }
	[Display(Name = "Transaction Type")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionType { get; init; } = "";
	[Display(Name = "Transaction Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionAmount { get; init; }
	[Display(Name = "Lot No")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LotNo { get; init; } = "";
	[Display(Name = "Line No")]
	public int? LineNo { get; init; }
	[Display(Name = "Tax Scheme")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaxScheme { get; init; } = "";
	[Display(Name = "Transaction Tax Base Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionTaxBaseAmount { get; init; }
	[Display(Name = "Transaction Tax Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionTaxAmount { get; init; }
	[Display(Name = "Sequence No")]
	public int? SequenceNo { get; init; }
	[Display(Name = "Reference No")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReferenceNo { get; init; } = "";
	[Display(Name = "Transaction Class")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionClass { get; init; } = "";
	[Display(Name = "GLA Account")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GLAccount { get; init; } = "";
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Trade Name")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TradeName { get; init; } = "";
	[Display(Name = "Transaction Desc")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionDesc { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	
}
