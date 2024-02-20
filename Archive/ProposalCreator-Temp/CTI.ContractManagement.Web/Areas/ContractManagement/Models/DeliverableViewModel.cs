using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record DeliverableViewModel : BaseViewModel
{	
	[Display(Name = "Project Category")]
	[Required]
	
	public string ProjectCategoryId { get; init; } = "";
	public string?  ForeignKeyProjectCategory { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectCategoryViewModel? ProjectCategory { get; init; }
		
	public IList<ProjectDeliverableViewModel>? ProjectDeliverableList { get; set; }
	public IList<ProjectPackageAdditionalDeliverableViewModel>? ProjectPackageAdditionalDeliverableList { get; set; }
	public IList<ProjectDeliverableHistoryViewModel>? ProjectDeliverableHistoryList { get; set; }
	public IList<ProjectPackageAdditionalDeliverableHistoryViewModel>? ProjectPackageAdditionalDeliverableHistoryList { get; set; }
	
}
