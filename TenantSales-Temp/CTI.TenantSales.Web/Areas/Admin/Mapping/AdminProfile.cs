using AutoMapper;
using CTI.TenantSales.Web.Areas.Admin.Commands.Entities;
using CTI.TenantSales.Web.Areas.Admin.Models;
using CTI.Common.Data;
using Microsoft.AspNetCore.Identity;
using CTI.TenantSales.Core.Identity;

namespace CTI.TenantSales.Web.Areas.Admin.Mapping;

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
