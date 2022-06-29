using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record MainModulePlaceHolderState : BaseEntity
{
	public string Code { get; init; } = "";
	
		
	public IList<SubDetailItemPlaceHolderState>? SubDetailItemPlaceHolderList { get; set; }
	public IList<SubDetailListPlaceHolderState>? SubDetailListPlaceHolderList { get; set; }
	
}
