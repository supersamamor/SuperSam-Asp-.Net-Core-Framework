using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Mapping
{
    public class AreaPlaceHolderProfile : Profile
    {
        public AreaPlaceHolderProfile()
        {
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<ProjectViewModel, EditProjectCommand>();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
