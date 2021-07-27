using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.MainModulePlaceHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping
{
    public class MainModulePlaceHolderProfile : Profile
    {
        public MainModulePlaceHolderProfile()
        {
            CreateMap<AddProjectCommand, Project>();
            CreateMap<EditProjectCommand, Project>();
        }
    }
}
