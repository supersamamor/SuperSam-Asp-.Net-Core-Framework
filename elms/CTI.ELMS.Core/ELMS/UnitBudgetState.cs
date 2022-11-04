using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UnitBudgetState : BaseEntity
{
	public int? Year { get; init; }
	public string? ProjectID { get; init; }
	public string? UnitID { get; init; }
	public decimal? January { get; init; }
	public decimal? February { get; init; }
	public decimal? March { get; init; }
	public decimal? April { get; init; }
	public decimal? May { get; init; }
	public decimal? June { get; init; }
	public decimal? July { get; init; }
	public decimal? August { get; init; }
	public decimal? September { get; init; }
	public decimal? October { get; init; }
	public decimal? November { get; init; }
	public decimal? December { get; init; }
	public decimal? LotArea { get; init; }
	public bool IsOriginalBudgeted { get; init; }
	public string? ParentUnitBudgetID { get; init; }
	
	public ProjectState? Project { get; init; }
	public UnitState? Unit { get; init; }
	public UnitBudgetState? UnitBudget { get; init; }
	
	public IList<UnitBudgetState>? UnitBudgetList { get; set; }
	
}
