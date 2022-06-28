using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<ModuleNamePlaceHolderViewModel, AddModuleNamePlaceHolderCommand>();
		CreateMap<ModuleNamePlaceHolderViewModel, EditModuleNamePlaceHolderCommand>();
		CreateMap<ModuleNamePlaceHolderState, ModuleNamePlaceHolderViewModel>().ReverseMap();
		
    }
}
