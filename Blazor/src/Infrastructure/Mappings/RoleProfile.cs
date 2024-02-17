using AutoMapper;
using ProjectNamePlaceHolder.Infrastructure.Models.Identity;
using ProjectNamePlaceHolder.Application.Responses.Identity;

namespace ProjectNamePlaceHolder.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}