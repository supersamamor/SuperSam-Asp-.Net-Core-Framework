using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ApproverSetup.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
        CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
        CreateMap<MainModulePlaceHolderState, MainModulePlaceHolderViewModel>().ReverseMap();
        CreateMap<SubDetailItemPlaceHolderViewModel, AddSubDetailItemPlaceHolderCommand>();
        CreateMap<SubDetailItemPlaceHolderViewModel, EditSubDetailItemPlaceHolderCommand>();
        CreateMap<SubDetailItemPlaceHolderState, SubDetailItemPlaceHolderViewModel>().ReverseMap();
        CreateMap<SubDetailListPlaceHolderViewModel, AddSubDetailListPlaceHolderCommand>();
        CreateMap<SubDetailListPlaceHolderViewModel, EditSubDetailListPlaceHolderCommand>();
        CreateMap<SubDetailListPlaceHolderState, SubDetailListPlaceHolderViewModel>().ReverseMap();


        CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
        CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
        CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
