using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<ProjectViewModel, EditProjectCommand>();
        }
    }
}
