using AutoMapper;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Commands.Entities;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Models;
using CNPlaceHolder.Common.Data;
using Microsoft.AspNetCore.Identity;
using CNPlaceHolder.PNPlaceHolder.Core.Identity;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Mapping;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Entity, EntityViewModel>().ReverseMap();
        CreateMap<EntityViewModel, AddOrEditEntityCommand>();
        CreateMap<AddOrEditEntityCommand, Entity>();

        CreateMap<ApplicationRole, RoleViewModel>().ReverseMap();

        CreateMap<Audit, AuditLogViewModel>();
        CreateMap<ApplicationUser, AuditLogUserViewModel>();
    }
}
