using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record MainModuleState : BaseEntity
{
	public string ParentModuleId { get; init; } = "";
	public string FileUpload { get; init; } = "";
	public string Code { get; init; } = "";
	
	public ParentModuleState? ParentModule { get; init; }
	
	public IList<SubDetailItemState>? SubDetailItemList { get; set; }
	public IList<SubDetailListState>? SubDetailListList { get; set; }
	
}
