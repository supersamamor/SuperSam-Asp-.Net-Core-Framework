using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
    }
}
