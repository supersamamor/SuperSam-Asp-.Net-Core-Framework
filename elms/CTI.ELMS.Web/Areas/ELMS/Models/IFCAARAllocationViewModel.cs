using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record IFCAARAllocationViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Tenant Contract No.")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantContractNo { get; init; } = "";
	[Display(Name = "Document No")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNo { get; init; } = "";
	[Display(Name = "Transaction Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionAmount { get; init; }
	[Display(Name = "Transaction Type")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionType { get; init; } = "";
	[Display(Name = "Document Date")]
	public DateTime? DocumentDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	
}
