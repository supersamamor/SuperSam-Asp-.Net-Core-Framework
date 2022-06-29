using AutoMapper;
using CTI.Common.Core.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolderState>();
		CreateMap <EditMainModulePlaceHolderCommand, MainModulePlaceHolderState>().IgnoreBaseEntityProperties();
		CreateMap<AddSubDetailItemPlaceHolderCommand, SubDetailItemPlaceHolderState>();
		CreateMap <EditSubDetailItemPlaceHolderCommand, SubDetailItemPlaceHolderState>().IgnoreBaseEntityProperties();
		CreateMap<AddSubDetailListPlaceHolderCommand, SubDetailListPlaceHolderState>();
		CreateMap <EditSubDetailListPlaceHolderCommand, SubDetailListPlaceHolderState>().IgnoreBaseEntityProperties();
		
    }
}
