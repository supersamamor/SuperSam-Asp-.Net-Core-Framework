using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ReportTableCollectionDetailViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "IFCA Tenant Information")]
	
	public string? IFCATenantInformationID { get; init; }
	public string?  ForeignKeyIFCATenantInformation { get; set; }
	[Display(Name = "Month")]
	public int? Month { get; init; }
	[Display(Name = "Year")]
	[Required]
	
	public string Year { get; init; } = "";
	[Display(Name = "Is Terminated")]
	public bool IsTerminated { get; init; }
	[Display(Name = "Current Month")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CurrentMonth { get; init; }
	[Display(Name = "Prev Month")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth1 { get; init; }
	[Display(Name = "Prev Month")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth2 { get; init; }
	[Display(Name = "Prev Month")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth3 { get; init; }
	[Display(Name = "Prior")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Prior { get; init; }
	[Display(Name = "Total Over")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalOverDue { get; init; }
	[Display(Name = "Grand Total")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? GrandTotal { get; init; }
	[Display(Name = "Rental")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Rental { get; init; }
	[Display(Name = "CusaAC")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CusaAC { get; init; }
	[Display(Name = "Utilities")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Utilities { get; init; }
	[Display(Name = "Deposits")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Deposits { get; init; }
	[Display(Name = "Interests")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Interests { get; init; }
	[Display(Name = "Penalty")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Penalty { get; init; }
	[Display(Name = "Others")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Others { get; init; }
	[Display(Name = "PaidSD")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PaidSD { get; init; }
	[Display(Name = "SDExposure")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? SDExposure { get; init; }
	[Display(Name = "Payable Current")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayableCurrentMonth { get; init; }
	[Display(Name = "Payable Prev")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth1 { get; init; }
	[Display(Name = "Payable Prev")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth2 { get; init; }
	[Display(Name = "Payable Prev")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth3 { get; init; }
	[Display(Name = "Payable Prior")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrior { get; init; }
	[Display(Name = "Column1")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column1 { get; init; } = "";
	[Display(Name = "Column2")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column2 { get; init; } = "";
	[Display(Name = "Column3")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column3 { get; init; } = "";
	[Display(Name = "Column4")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column4 { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
	public IFCATenantInformationViewModel? IFCATenantInformation { get; init; }
		
	
}
