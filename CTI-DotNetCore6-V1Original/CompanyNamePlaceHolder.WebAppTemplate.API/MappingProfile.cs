using AutoMapper;
using CompanyNamePlaceHolder.WebAppTemplate.API.Controllers.v1;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory.Projects.Commands;

namespace CompanyNamePlaceHolder.WebAppTemplate.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
    }
}
