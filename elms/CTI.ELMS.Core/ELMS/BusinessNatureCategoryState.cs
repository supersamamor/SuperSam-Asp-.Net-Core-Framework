using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record BusinessNatureCategoryState : BaseEntity
{
	public string? BusinessNatureCategoryCode { get; init; }
	public string? BusinessNatureCategoryName { get; init; }
	public string? BusinessNatureSubItemID { get; init; }
	public bool IsDisabled { get; init; }
	
	public BusinessNatureSubItemState? BusinessNatureSubItem { get; init; }
	
	public IList<LeadState>? LeadList { get; set; }
	
}
