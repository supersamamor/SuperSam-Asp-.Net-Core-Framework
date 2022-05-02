using AutoMapper;
using CTI.WebAppTemplate.Infrastructure.Models;
using CTI.WebAppTemplate.Web.Areas.Admin.Commands.Entities;
using CTI.WebAppTemplate.Web.Areas.Admin.Models;
using CTI.WebAppTemplate.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CTI.WebAppTemplate.Web.Areas.Admin.Mapping;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Entity, EntityViewModel>().ReverseMap();
        CreateMap<EntityViewModel, AddOrEditEntityCommand>();
        CreateMap<AddOrEditEntityCommand, Entity>();

        CreateMap<IdentityRole, RoleViewModel>().ReverseMap();

        CreateMap<Audit, AuditLogViewModel>();
        CreateMap<ApplicationUser, AuditLogUserViewModel>();
    }
}
