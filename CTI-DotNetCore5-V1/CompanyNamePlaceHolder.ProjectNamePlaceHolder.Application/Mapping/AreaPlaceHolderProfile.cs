using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping
{
    public class AreaPlaceHolderProfile : Profile
    {
        public AreaPlaceHolderProfile()
        {
            CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolder>();
            CreateMap<EditMainModulePlaceHolderCommand, MainModulePlaceHolder>();
        }
    }
}
