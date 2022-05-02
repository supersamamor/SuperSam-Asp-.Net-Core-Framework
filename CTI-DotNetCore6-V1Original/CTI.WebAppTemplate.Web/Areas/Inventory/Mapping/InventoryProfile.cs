using AutoMapper;
using CTI.WebAppTemplate.Application.Features.Inventory.Projects.Commands;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Web.Areas.Inventory.Models;

namespace CTI.WebAppTemplate.Web.Areas.Inventory.Mapping;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
        CreateMap<ProjectState, ProjectViewModel>();
    }
}
