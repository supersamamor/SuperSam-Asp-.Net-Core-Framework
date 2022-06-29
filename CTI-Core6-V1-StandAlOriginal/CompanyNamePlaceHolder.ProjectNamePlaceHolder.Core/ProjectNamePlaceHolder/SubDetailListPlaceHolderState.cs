using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record SubDetailListPlaceHolderState : BaseEntity
{
	public string MainModulePlaceHolderId { get; init; } = "";
	public string Code { get; init; } = "";
	
	public MainModulePlaceHolderState? MainModulePlaceHolder { get; init; }
		
	
}
