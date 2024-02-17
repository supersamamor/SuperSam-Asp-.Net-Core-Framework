using AutoMapper;
using ProjectNamePlaceHolder.Application.Requests.Identity;
using ProjectNamePlaceHolder.Application.Responses.Identity;

namespace ProjectNamePlaceHolder.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}