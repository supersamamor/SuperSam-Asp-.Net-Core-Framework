using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;



namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<AddMainModuleCommand, MainModuleState>();
		CreateMap <EditMainModuleCommand, MainModuleState>().IgnoreBaseEntityProperties();
		CreateMap<AddParentModuleCommand, ParentModuleState>();
		CreateMap <EditParentModuleCommand, ParentModuleState>().IgnoreBaseEntityProperties();
		CreateMap<AddSubDetailItemCommand, SubDetailItemState>();
		CreateMap <EditSubDetailItemCommand, SubDetailItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddSubDetailListCommand, SubDetailListState>();
		CreateMap <EditSubDetailListCommand, SubDetailListState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
