using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
        CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
		CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
		CreateMap<MainModulePlaceHolderState, MainModulePlaceHolderViewModel>().ReverseMap();
		CreateMap<SubDetailItemPlaceHolderViewModel, AddSubDetailItemPlaceHolderCommand>();
		CreateMap<SubDetailItemPlaceHolderViewModel, EditSubDetailItemPlaceHolderCommand>();
		CreateMap<SubDetailItemPlaceHolderState, SubDetailItemPlaceHolderViewModel>().ReverseMap();
		CreateMap<SubDetailListPlaceHolderViewModel, AddSubDetailListPlaceHolderCommand>();
		CreateMap<SubDetailListPlaceHolderViewModel, EditSubDetailListPlaceHolderCommand>();
		CreateMap<SubDetailListPlaceHolderState, SubDetailListPlaceHolderViewModel>().ReverseMap();
		
    }
}
