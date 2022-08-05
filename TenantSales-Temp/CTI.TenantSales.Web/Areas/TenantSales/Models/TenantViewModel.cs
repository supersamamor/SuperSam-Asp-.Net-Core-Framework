using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record TenantViewModel : BaseViewModel
{	
	[Display(Name = "Rental Type")]
	[Required]
	
	public string RentalTypeId { get; init; } = "";
	public string?  ForeignKeyRentalType { get; set; }
	[Display(Name = "Project")]
	[Required]
	
	public string ProjectId { get; init; } = "";
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "File Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FileCode { get; init; }
	[Display(Name = "Folder")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Folder { get; init; }
	[Display(Name = "Date From")]
	[Required]
	public DateTime DateFrom { get; init; } = DateTime.Now.Date;
	[Display(Name = "Date To")]
	[Required]
	public DateTime DateTo { get; init; } = DateTime.Now.Date;
	[Display(Name = "Opening")]
	[Required]
	public DateTime Opening { get; init; } = DateTime.Now.Date;
	[Display(Name = "Level")]
	
	public string? LevelId { get; init; }
	public string?  ForeignKeyLevel { get; set; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "Branch Contact")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BranchContact { get; init; }
	[Display(Name = "Head Office Contact")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? HeadOfficeContact { get; init; }
	[Display(Name = "IT Support Contact")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ITSupportContact { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public RentalTypeViewModel? RentalType { get; init; }
	public ProjectViewModel? Project { get; init; }
	public LevelViewModel? Level { get; init; }
		
	public IList<TenantLotViewModel>? TenantLotList { get; set; }
	public IList<SalesCategoryViewModel>? SalesCategoryList { get; set; }
	public IList<TenantContactViewModel>? TenantContactList { get; set; }
	public IList<TenantPOSViewModel>? TenantPOSList { get; set; }
	public IList<RevalidateViewModel>? RevalidateList { get; set; }
	public IList<TenantContactViewModel>? TenantBranchContactList 
	{
		get
		{
			return TenantContactList != null ? this.TenantContactList
				.Where(l => l.Group == Convert.ToInt32(Core.TenantSales.ContactGroup.Branch)).ToList() : null;
		}
	}
	public IList<TenantContactViewModel>? TenantITSupportContactList
	{
		get
		{
			return TenantContactList != null ? this.TenantContactList
				.Where(l => l.Group == Convert.ToInt32(Core.TenantSales.ContactGroup.ITSupport)).ToList() : null;
		}
	}
	public IList<TenantContactViewModel>? TenantHeadOfficeContactList
	{
		get
		{
			return TenantContactList != null ? this.TenantContactList
				.Where(l => l.Group == Convert.ToInt32(Core.TenantSales.ContactGroup.HeadOffice)).ToList() : null;
		}
	}
}
