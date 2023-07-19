using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.EISPL.Core.EISPL;

public record TestState : BaseEntity
{
	public string PLEmployeeId { get; init; } = "";
	public string TestColumn { get; init; } = "";
	
	public PLEmployeeState? PLEmployee { get; init; }
	
	
}
