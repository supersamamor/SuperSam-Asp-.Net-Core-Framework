using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectDeliverableHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectHistoryId { get; init; } = "";
	public string?  ForeignKeyProjectHistory { get; set; }
	[Display(Name = "Deliverable")]
	[Required]
	
	public string DeliverableId { get; init; } = "";
	public string?  ForeignKeyDeliverable { get; set; }
	[Display(Name = "Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get; init; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectHistoryViewModel? ProjectHistory { get; init; }
	public DeliverableViewModel? Deliverable { get; init; }
		
	
}
