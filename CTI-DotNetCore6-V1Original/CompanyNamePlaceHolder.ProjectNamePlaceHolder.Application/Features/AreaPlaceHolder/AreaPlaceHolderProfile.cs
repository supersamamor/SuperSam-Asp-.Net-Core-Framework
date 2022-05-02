using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
    {
        CreateMap<AddProjectCommand, ProjectState>();
        CreateMap<EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
    }
}
