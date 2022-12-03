using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record UserEntityState : BaseEntity
{
	public string PplusUserId { get; init; } = "";
	public string CompanyId { get; init; } = "";
	
	public CompanyState? Company { get; init; }
	
	
}
