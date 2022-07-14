using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record SubDetailItemState : BaseEntity
{
	public string TestForeignKeyTwo { get; init; } = "";
	public string Code { get; init; } = "";
	
	public MainModuleState? MainModule { get; init; }
	
	
}
