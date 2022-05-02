using AutoMapper;
using CTI.WebAppTemplate.API.Controllers.v1;
using CTI.WebAppTemplate.Application.Features.Inventory.Projects.Commands;

namespace CTI.WebAppTemplate.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
    }
}
