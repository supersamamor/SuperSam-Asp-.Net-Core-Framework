using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ApproverSetup.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CTI.Common.Core.Mapping;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolderState>();
        CreateMap<EditMainModulePlaceHolderCommand, MainModulePlaceHolderState>().IgnoreBaseEntityProperties();
        CreateMap<AddSubDetailItemPlaceHolderCommand, SubDetailItemPlaceHolderState>();
        CreateMap<EditSubDetailItemPlaceHolderCommand, SubDetailItemPlaceHolderState>().IgnoreBaseEntityProperties();
        CreateMap<AddSubDetailListPlaceHolderCommand, SubDetailListPlaceHolderState>();
        CreateMap<EditSubDetailListPlaceHolderCommand, SubDetailListPlaceHolderState>().IgnoreBaseEntityProperties();


        CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
        CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
        CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
