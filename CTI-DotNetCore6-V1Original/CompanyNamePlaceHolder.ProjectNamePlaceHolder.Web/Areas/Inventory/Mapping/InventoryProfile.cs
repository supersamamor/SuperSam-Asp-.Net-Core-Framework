using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Mapping;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
        CreateMap<ProjectState, ProjectViewModel>();
    }
}
