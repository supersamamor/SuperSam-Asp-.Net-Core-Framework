using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping
{
    public class ProjectNamePlaceHolderProfile : Profile
    {
        public ProjectNamePlaceHolderProfile()
        {
            CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
                     CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
                     CreateMap<MainModulePlaceHolder, MainModulePlaceHolderViewModel>().ReverseMap();
                    
        }
    }
}
