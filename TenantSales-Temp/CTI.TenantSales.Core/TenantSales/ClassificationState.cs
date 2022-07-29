using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record ClassificationState : BaseEntity
{
	public string ThemeId { get; init; } = "";
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	
	public ThemeState? Theme { get; init; }
	
	public IList<CategoryState>? CategoryList { get; set; }
	
}
