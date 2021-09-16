using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder
{
    public class AreaPlaceHolderProfile : Profile
    {
        public AreaPlaceHolderProfile()
        {
            CreateMap<AddMainModulePlaceHolderCommand, Core.AreaPlaceHolder.MainModulePlaceHolder>();
            CreateMap<EditMainModulePlaceHolderCommand, Core.AreaPlaceHolder.MainModulePlaceHolder>().IgnoreBaseEntityProperties();
        }
    }
}
