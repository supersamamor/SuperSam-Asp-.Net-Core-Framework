using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Mapping;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
    {
        CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
        CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
        CreateMap<MainModulePlaceHolderState, MainModulePlaceHolderViewModel>();
    }
}
