using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record IFCATenantInformationViewModel : BaseViewModel
{	
	[Display(Name = "Offering")]
	[Required]
	
	public string OfferingID { get; init; } = "";
	public string?  ForeignKeyOffering { get; set; }
	[Display(Name = "Tenant Contract No.")]
	[Required]
	
	public string TenantContractNo { get; init; } = "";
	[Display(Name = "Is Exhibit")]
	public bool IsExhibit { get; init; }
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectID { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Trade Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TradeName { get; init; } = "";
	[Display(Name = "TIN Number")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TINNumber { get; init; } = "";
	[Display(Name = "Paid Security")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PaidSecurityDeposit { get; init; }
	[Display(Name = "Allowance")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Allowance { get; init; }
	[Display(Name = "Tenant Category")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantCategory { get; init; } = "";
	[Display(Name = "Is Anchor")]
	public bool IsAnchor { get; init; }
	[Display(Name = "Tenant Classification")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantClassification { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public OfferingViewModel? Offering { get; init; }
	public ProjectViewModel? Project { get; init; }
		
	public IList<IFCAUnitInformationViewModel>? IFCAUnitInformationList { get; set; }
	public IList<ReportTableCollectionDetailViewModel>? ReportTableCollectionDetailList { get; set; }
	
}
