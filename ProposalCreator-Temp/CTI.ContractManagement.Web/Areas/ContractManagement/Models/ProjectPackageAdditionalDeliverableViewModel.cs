using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectPackageAdditionalDeliverableViewModel : BaseViewModel
{	
	[Display(Name = "Project Package")]
	[Required]
	
	public string ProjectPackageId { get; init; } = "";
	public string?  ForeignKeyProjectPackage { get; set; }
	[Display(Name = "Deliverable")]
	[Required]
	
	public string DeliverableId { get; init; } = "";
	public string?  ForeignKeyDeliverable { get; set; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectPackageViewModel? ProjectPackage { get; init; }
	public DeliverableViewModel? Deliverable { get; init; }
		
	
}
