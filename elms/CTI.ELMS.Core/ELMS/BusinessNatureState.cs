using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record BusinessNatureState : BaseEntity
{
	public string BusinessNatureName { get; init; } = "";
	public string BusinessNatureCode { get; init; } = "";
	public bool IsDisabled { get; init; }
	
	
	public IList<BusinessNatureSubItemState>? BusinessNatureSubItemList { get; set; }
	public IList<LeadState>? LeadList { get; set; }
	
}
