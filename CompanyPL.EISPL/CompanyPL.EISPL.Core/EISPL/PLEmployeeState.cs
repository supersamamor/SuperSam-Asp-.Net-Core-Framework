using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.EISPL.Core.EISPL;

public record PLEmployeeState : BaseEntity
{
	public string PLFirstName { get; init; } = "";
	public string PLMiddleName { get; init; } = "";
	public string PLEmployeeCode { get; init; } = "";
	public string PLLastName { get; init; } = "";
	
	
	public IList<PLContactInformationState>? PLContactInformationList { get; set; }
	public IList<PLHealthDeclarationState>? PLHealthDeclarationList { get; set; }
	public IList<TestState>? TestList { get; set; }
	
}
