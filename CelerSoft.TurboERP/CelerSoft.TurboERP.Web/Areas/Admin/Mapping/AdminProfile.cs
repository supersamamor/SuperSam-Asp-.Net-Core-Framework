using AutoMapper;
using CelerSoft.TurboERP.Web.Areas.Admin.Commands.Entities;
using CelerSoft.TurboERP.Web.Areas.Admin.Models;
using CelerSoft.Common.Data;
using Microsoft.AspNetCore.Identity;
using CelerSoft.TurboERP.Core.Identity;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Mapping;

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
