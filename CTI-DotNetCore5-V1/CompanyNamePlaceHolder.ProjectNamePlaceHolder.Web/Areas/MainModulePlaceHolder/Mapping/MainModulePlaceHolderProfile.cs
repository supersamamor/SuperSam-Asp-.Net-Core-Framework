using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.MainModulePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.MainModulePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.MainModulePlaceHolder.Pages.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.MainModulePlaceHolder.Mapping
{
    public class MainModulePlaceHolderProfile : Profile
    {
        public MainModulePlaceHolderProfile()
        {
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<ProjectViewModel, EditProjectCommand>();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
