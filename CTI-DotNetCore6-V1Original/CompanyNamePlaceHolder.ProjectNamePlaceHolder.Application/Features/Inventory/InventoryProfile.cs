using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<AddProjectCommand, ProjectState>();
        CreateMap<EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
    }
}
