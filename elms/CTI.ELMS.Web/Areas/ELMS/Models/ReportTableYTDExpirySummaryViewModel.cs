using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ReportTableYTDExpirySummaryViewModel : BaseViewModel
{	
	[Display(Name = "EntityID")]
	public int? EntityID { get; init; }
	[Display(Name = "Entity Short")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityShortName { get; init; } = "";
	[Display(Name = "Entity Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityName { get; init; } = "";
	[Display(Name = "ProjectID")]
	public int? ProjectID { get; init; }
	[Display(Name = "Project Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectName { get; init; } = "";
	[Display(Name = "Location")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Location { get; init; } = "";
	[Display(Name = "Land Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LandArea { get; init; }
	[Display(Name = "TotalGLA")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalGLA { get; init; }
	[Display(Name = "Column Name")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ColumnName { get; init; } = "";
	[Display(Name = "Expiry Lot")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ExpiryLotArea { get; init; }
	[Display(Name = "Renewed")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Renewed { get; init; }
	[Display(Name = "New Leases")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? NewLeases { get; init; }
	[Display(Name = "With Prospect")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? WithProspectNego { get; init; }
	[Display(Name = "Order By")]
	public int? OrderBy { get; init; }
	[Display(Name = "Vertical Order")]
	public int? VerticalOrderBy { get; init; }
	[Display(Name = "Report Year")]
	public int? ReportYear { get; init; }
	[Display(Name = "Processed Date")]
	public DateTime? ProcessedDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
