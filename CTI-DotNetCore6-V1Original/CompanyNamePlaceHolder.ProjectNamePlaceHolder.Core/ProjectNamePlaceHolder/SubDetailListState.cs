using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record SubDetailListState : BaseEntity
{
	public string Code { get; init; } = "";
	public string TestForeignKeyOne { get; init; } = "";
	
	public MainModuleState? MainModule { get; init; }
	
	
}
