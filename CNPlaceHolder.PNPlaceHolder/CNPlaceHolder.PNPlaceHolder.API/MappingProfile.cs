using AutoMapper;
using CNPlaceHolder.PNPlaceHolder.API.Controllers.v1;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;


namespace CNPlaceHolder.PNPlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TnamePlaceHolderViewModel, AddTnamePlaceHolderCommand>();
		CreateMap <TnamePlaceHolderViewModel, EditTnamePlaceHolderCommand>();
		
    }
}
