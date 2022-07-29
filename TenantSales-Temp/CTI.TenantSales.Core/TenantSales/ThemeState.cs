using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record ThemeState : BaseEntity
{
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	
	
	public IList<ClassificationState>? ClassificationList { get; set; }
	
}
