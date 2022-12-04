using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record DatabaseConnectionSetupState : BaseEntity
{
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	public string DatabaseAndServerName { get; init; } = "";
	public string? InhouseDatabaseAndServerName { get; init; }
	public string? SystemConnectionString { get; init; }
	public bool IsDisabled { get; init; }
	
	
	public IList<CompanyState>? CompanyList { get; set; }
	public IList<CreditorState>? CreditorList { get; set; }
	
}
