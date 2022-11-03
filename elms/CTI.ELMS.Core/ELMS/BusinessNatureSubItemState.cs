using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record BusinessNatureSubItemState : BaseEntity
{
	public string? BusinessNatureSubItemName { get; init; }
	public string? BusinessNatureID { get; init; }
	public bool IsDisabled { get; init; }
	public string? BusinessNatureSubItemCode { get; init; }
	
	public BusinessNatureState? BusinessNature { get; init; }
	
	public IList<BusinessNatureCategoryState>? BusinessNatureCategoryList { get; set; }
	public IList<LeadState>? LeadList { get; set; }
	
}
