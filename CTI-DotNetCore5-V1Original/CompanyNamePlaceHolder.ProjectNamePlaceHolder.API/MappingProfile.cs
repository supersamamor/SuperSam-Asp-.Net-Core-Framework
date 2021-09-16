using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
                     CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
                    
        }
    }
}
