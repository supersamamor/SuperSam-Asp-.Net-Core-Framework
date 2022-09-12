using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectPackageAdditionalDeliverableHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Project Package")]
	[Required]
	
	public string ProjectPackageHistoryId { get; init; } = "";
	public string?  ForeignKeyProjectPackageHistory { get; set; }
	[Display(Name = "Deliverable")]
	[Required]
	
	public string DeliverableId { get; init; } = "";
	public string?  ForeignKeyDeliverable { get; set; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectPackageHistoryViewModel? ProjectPackageHistory { get; init; }
	public DeliverableViewModel? Deliverable { get; init; }
		
	
}
