using AutoMapper;
using CNPlaceHolder.PNPlaceHolder.API.Controllers.v1;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;


namespace CNPlaceHolder.PNPlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ModPlaceHolderViewModel, AddModPlaceHolderCommand>();
		CreateMap <ModPlaceHolderViewModel, EditModPlaceHolderCommand>();
		
    }
}
