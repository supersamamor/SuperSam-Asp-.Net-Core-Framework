using AutoMapper;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Mapping;
using CompanyNamePlaceHolder.WebAppTemplate.Core.Inventory;

namespace CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<AddProjectCommand, ProjectState>();
        CreateMap<EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
    }
}
