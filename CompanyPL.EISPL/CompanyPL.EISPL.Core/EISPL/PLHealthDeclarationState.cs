using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.EISPL.Core.EISPL;

public record PLHealthDeclarationState : BaseEntity
{
	public string? PLVaccine { get; init; }
	public bool? PLIsVaccinated { get; init; }
	public string PLEmployeeId { get; init; } = "";
	
	public PLEmployeeState? PLEmployee { get; init; }
	
	
}
