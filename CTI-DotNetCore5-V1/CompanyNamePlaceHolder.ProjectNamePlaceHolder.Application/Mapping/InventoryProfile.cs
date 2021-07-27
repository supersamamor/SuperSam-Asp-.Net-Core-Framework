using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<AddProjectCommand, Project>();
            CreateMap<EditProjectCommand, Project>();
        }
    }
}
