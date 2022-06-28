using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<AddModuleNamePlaceHolderCommand, ModuleNamePlaceHolderState>();
		CreateMap <EditModuleNamePlaceHolderCommand, ModuleNamePlaceHolderState>().IgnoreBaseEntityProperties();
		
    }
}
