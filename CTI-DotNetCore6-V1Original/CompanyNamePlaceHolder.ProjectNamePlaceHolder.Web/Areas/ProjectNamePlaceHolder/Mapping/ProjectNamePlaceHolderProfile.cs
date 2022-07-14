using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<MainModuleViewModel, AddMainModuleCommand>();
		CreateMap<MainModuleViewModel, EditMainModuleCommand>();
		CreateMap<MainModuleState, MainModuleViewModel>().ReverseMap();
		CreateMap<ParentModuleViewModel, AddParentModuleCommand>();
		CreateMap<ParentModuleViewModel, EditParentModuleCommand>();
		CreateMap<ParentModuleState, ParentModuleViewModel>().ReverseMap();
		CreateMap<SubDetailItemViewModel, AddSubDetailItemCommand>();
		CreateMap<SubDetailItemViewModel, EditSubDetailItemCommand>();
		CreateMap<SubDetailItemState, SubDetailItemViewModel>().ReverseMap();
		CreateMap<SubDetailListViewModel, AddSubDetailListCommand>();
		CreateMap<SubDetailListViewModel, EditSubDetailListCommand>();
		CreateMap<SubDetailListState, SubDetailListViewModel>().ReverseMap();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
