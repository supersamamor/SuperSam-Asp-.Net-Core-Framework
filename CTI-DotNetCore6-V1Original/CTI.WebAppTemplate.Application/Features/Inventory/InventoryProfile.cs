using AutoMapper;
using CTI.WebAppTemplate.Application.Features.Inventory.Projects.Commands;
using CTI.WebAppTemplate.Application.Mapping;
using CTI.WebAppTemplate.Core.Inventory;

namespace CTI.WebAppTemplate.Application.Features.Inventory;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<AddProjectCommand, ProjectState>();
        CreateMap<EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
    }
}
