using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record SalutationState : BaseEntity
{
	public string SalutationDescription { get; init; } = "";
	
	
	public IList<ContactPersonState>? ContactPersonList { get; set; }
	
}
