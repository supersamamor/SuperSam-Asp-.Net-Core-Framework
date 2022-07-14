using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MainModuleViewModel, AddMainModuleCommand>();
		CreateMap <MainModuleViewModel, EditMainModuleCommand>();
		CreateMap<ParentModuleViewModel, AddParentModuleCommand>();
		CreateMap <ParentModuleViewModel, EditParentModuleCommand>();
		CreateMap<SubDetailItemViewModel, AddSubDetailItemCommand>();
		CreateMap <SubDetailItemViewModel, EditSubDetailItemCommand>();
		CreateMap<SubDetailListViewModel, AddSubDetailListCommand>();
		CreateMap <SubDetailListViewModel, EditSubDetailListCommand>();
		
    }
}
