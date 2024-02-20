using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Models;

public record ProjectViewModel : BaseViewModel
{	
	[Display(Name = "Client")]
	[Required]
	
	public string ClientId { get; init; } = "";
	public string?  ForeignKeyClient { get; set; }
	[Display(Name = "Project Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectName { get; init; } = "";
	[Display(Name = "Description")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectDescription { get; init; } = "";
	[Display(Name = "Logo")]
	
	public IFormFile? LogoForm { get; set; }public string? Logo { get; init; }
	public string? GeneratedLogoPath
	{
		get
		{
			return this.LogoForm?.FileName == null ? this.Logo : "\\" + WebConstants.Project + "\\" + this.Id + "\\" + nameof(this.Logo) + "\\" + this.LogoForm!.FileName;
		}
	}
	[Display(Name = "Goals")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectGoals { get; init; } = "";
	[Display(Name = "Discount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Discount { get; init; }
	[Display(Name = "Pricing Type")]
	[Required]
	
	public string PricingTypeId { get; init; } = "";
	public string?  ForeignKeyPricingType { get; set; }
	[Display(Name = "Enable Pricing")]
	public bool EnablePricing { get; init; }
	[Display(Name = "Template")]
	
	public string? Template { get; init; }
	[Display(Name = "Summary of Revision")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RevisionSummary { get; init; } = "";
	[Display(Name = "Document Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentCode { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ClientViewModel? Client { get; init; }
	public PricingTypeViewModel? PricingType { get; init; }
		
	public IList<ProjectDeliverableViewModel>? ProjectDeliverableList { get; set; }
	public IList<ProjectMilestoneViewModel>? ProjectMilestoneList { get; set; }
	public IList<ProjectPackageViewModel>? ProjectPackageList { get; set; }
	public IList<ProjectHistoryViewModel>? ProjectHistoryList { get; set; }
	
}
