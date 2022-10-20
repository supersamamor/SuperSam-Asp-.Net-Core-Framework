using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;

public record SubDetailListState : BaseEntity
{
	public string Code { get; init; } = "";
	public string TestForeignKeyOne { get; init; } = "";
	
	public MainModuleState? MainModule { get; init; }
	
	
}
