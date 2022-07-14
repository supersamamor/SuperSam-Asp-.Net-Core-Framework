using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record ParentModuleState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<MainModuleState>? MainModuleList { get; set; }
	
}
