using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping
{
    public class ProjectNamePlaceHolderProfile : Profile
    {
        public ProjectNamePlaceHolderProfile()
        {
            CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolder>();
                     CreateMap<EditMainModulePlaceHolderCommand, MainModulePlaceHolder>();
                    
        }
    }
}
