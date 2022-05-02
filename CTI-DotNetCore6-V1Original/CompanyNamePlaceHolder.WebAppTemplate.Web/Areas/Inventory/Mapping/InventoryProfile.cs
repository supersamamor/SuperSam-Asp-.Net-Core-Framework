using AutoMapper;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.WebAppTemplate.Core.Inventory;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Inventory.Models;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Inventory.Mapping;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
        CreateMap<ProjectState, ProjectViewModel>();
    }
}
