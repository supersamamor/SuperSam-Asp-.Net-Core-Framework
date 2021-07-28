using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;

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
