using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;



namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolderState>();
		CreateMap <EditMainModulePlaceHolderCommand, MainModulePlaceHolderState>().IgnoreBaseEntityProperties();
		
		
    }
}
