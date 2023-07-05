using AutoMapper;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;

using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;


namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Mapping;

public class PNPlaceHolderProfile : Profile
{
    public PNPlaceHolderProfile()
    {
        CreateMap<TnamePlaceHolderViewModel, AddTnamePlaceHolderCommand>();
		CreateMap<TnamePlaceHolderViewModel, EditTnamePlaceHolderCommand>();
		CreateMap<TnamePlaceHolderState, TnamePlaceHolderViewModel>().ReverseMap();
		
		
    }
}
