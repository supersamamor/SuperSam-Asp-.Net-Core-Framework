using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record UnitViewModel : BaseViewModel
{	
	[Display(Name = "Unit No")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string UnitNo { get; init; } = "";
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectID { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Lot Budget")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LotBudget { get; init; }
	[Display(Name = "Lot Area")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LotArea { get; init; }
	[Display(Name = "Availability Date")]
	public DateTime? AvailabilityDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Commencement Date")]
	public DateTime? CommencementDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Current Tenant Contract No.")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CurrentTenantContractNo { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	public IList<UnitBudgetViewModel>? UnitBudgetList { get; set; }
	public IList<UnitActivityViewModel>? UnitActivityList { get; set; }
	public IList<PreSelectedUnitViewModel>? PreSelectedUnitList { get; set; }
	public IList<UnitOfferedViewModel>? UnitOfferedList { get; set; }
	public IList<UnitOfferedViewModel>? UnitOfferedHistoryList { get; set; }
	public IList<IFCAUnitInformationViewModel>? IFCAUnitInformationList { get; set; }
	
}
