using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record TenantState : BaseEntity
{
	public string RentalTypeId { get; init; } = "";
	public string ProjectId { get; init; } = "";
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	public string? FileCode { get; init; }
	public string? Folder { get; init; }
	public DateTime DateFrom { get; init; }
	public DateTime DateTo { get; init; }
	public DateTime Opening { get; init; }
	public string? LevelId { get; init; }
	public bool IsDisabled { get; init; }
	public string? BranchContact { get; init; }
	public string? HeadOfficeContact { get; init; }
	public string? ITSupportContact { get; init; }
	public string? CategoryId { get; init; }
	public decimal Area { get; init; }	
	public RentalTypeState? RentalType { get; init; }
	public ProjectState? Project { get; init; }
	public LevelState? Level { get; init; }
	public CategoryState? Category { get; init; }
	public IList<TenantLotState>? TenantLotList { get; set; }
	public IList<SalesCategoryState>? SalesCategoryList { get; set; }
	public IList<TenantContactState>? TenantContactList { get; set; }
	public IList<TenantPOSState>? TenantPOSList { get; set; }
	public IList<RevalidateState>? RevalidateList { get; set; }
	
}
