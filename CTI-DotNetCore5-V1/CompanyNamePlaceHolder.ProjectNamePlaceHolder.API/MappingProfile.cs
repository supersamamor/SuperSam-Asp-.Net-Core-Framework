using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands;
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
            CreateMap<ProjectViewModel, AddMainModulePlaceHolderCommand>();
            CreateMap<ProjectViewModel, EditMainModulePlaceHolderCommand>();
        }
    }
}
