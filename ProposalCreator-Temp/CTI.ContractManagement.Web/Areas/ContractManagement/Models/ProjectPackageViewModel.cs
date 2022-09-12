using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectPackageViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Option")]
	public int? OptionNumber { get; init; }
	[Display(Name = "Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	public IList<ProjectPackageAdditionalDeliverableViewModel>? ProjectPackageAdditionalDeliverableList { get; set; }
	
}
