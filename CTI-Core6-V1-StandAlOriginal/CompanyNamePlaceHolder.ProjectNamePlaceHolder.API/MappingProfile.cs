using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ModuleNamePlaceHolderViewModel, AddModuleNamePlaceHolderCommand>();
		CreateMap <ModuleNamePlaceHolderViewModel, EditModuleNamePlaceHolderCommand>();
		
    }
}
