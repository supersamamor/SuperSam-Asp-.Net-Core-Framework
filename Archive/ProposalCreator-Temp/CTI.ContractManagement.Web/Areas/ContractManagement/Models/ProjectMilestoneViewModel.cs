using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectMilestoneViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Milestone Stage")]
	[Required]
	
	public string MilestoneStageId { get; init; } = "";
	public string?  ForeignKeyMilestoneStage { get; set; }
	[Display(Name = "Sequence")]
	public int? Sequence { get; init; }
	[Display(Name = "Frequency")]
	[Required]
	
	public string FrequencyId { get; init; } = "";
	public string?  ForeignKeyFrequency { get; set; }
	[Display(Name = "Quantity")]
	public int? FrequencyQuantity { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
	public MilestoneStageViewModel? MilestoneStage { get; init; }
	public FrequencyViewModel? Frequency { get; init; }
		
	
}
