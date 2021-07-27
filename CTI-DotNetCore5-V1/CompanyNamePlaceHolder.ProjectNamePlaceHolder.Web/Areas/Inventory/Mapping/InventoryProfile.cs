using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Pages.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Mapping
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<ProjectViewModel, EditProjectCommand>();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
