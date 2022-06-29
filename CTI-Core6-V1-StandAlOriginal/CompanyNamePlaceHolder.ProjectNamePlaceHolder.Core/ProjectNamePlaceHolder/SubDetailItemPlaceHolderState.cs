using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record SubDetailItemPlaceHolderState : BaseEntity
{
	public string MainModulePlaceHolderId { get; init; } = "";
	public string Code { get; init; } = "";
	
	public MainModulePlaceHolderState? MainModulePlaceHolder { get; init; }
		
	
}
