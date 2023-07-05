using AutoMapper;
using CNPlaceHolder.Common.Core.Mapping;

using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;



namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder;

public class PNPlaceHolderProfile : Profile
{
    public PNPlaceHolderProfile()
    {
        CreateMap<AddTnamePlaceHolderCommand, TnamePlaceHolderState>();
		CreateMap <EditTnamePlaceHolderCommand, TnamePlaceHolderState>().IgnoreBaseEntityProperties();
		
		
    }
}
