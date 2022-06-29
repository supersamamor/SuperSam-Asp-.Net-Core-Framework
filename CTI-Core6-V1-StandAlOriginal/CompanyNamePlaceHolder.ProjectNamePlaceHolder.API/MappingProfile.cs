using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
		CreateMap <MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
		CreateMap<SubDetailItemPlaceHolderViewModel, AddSubDetailItemPlaceHolderCommand>();
		CreateMap <SubDetailItemPlaceHolderViewModel, EditSubDetailItemPlaceHolderCommand>();
		CreateMap<SubDetailListPlaceHolderViewModel, AddSubDetailListPlaceHolderCommand>();
		CreateMap <SubDetailListPlaceHolderViewModel, EditSubDetailListPlaceHolderCommand>();
		
    }
}
