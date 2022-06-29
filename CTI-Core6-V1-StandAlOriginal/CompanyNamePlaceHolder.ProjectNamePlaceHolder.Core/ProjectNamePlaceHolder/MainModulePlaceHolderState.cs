using CTI.Common.Core.Base.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record MainModulePlaceHolderState : BaseEntity
{
    public string Code { get; init; } = "";


    public IList<SubDetailItemPlaceHolderState>? SubDetailItemPlaceHolderList { get; set; }
    public IList<SubDetailListPlaceHolderState>? SubDetailListPlaceHolderList { get; set; }

}
