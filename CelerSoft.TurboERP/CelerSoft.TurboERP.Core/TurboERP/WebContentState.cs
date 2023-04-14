using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record WebContentState : BaseEntity
{
	public string? Code { get; init; }
	public string Content { get; init; } = "";
	public string? PageName { get; init; }
	public string? PageTitle { get; init; }
	
	
	
}
