using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.EISPL.Core.EISPL;

public record PLContactInformationState : BaseEntity
{
	public string PLContactDetails { get; init; } = "";
	public string PLEmployeeId { get; init; } = "";
	
	public PLEmployeeState? PLEmployee { get; init; }
	
	
}
