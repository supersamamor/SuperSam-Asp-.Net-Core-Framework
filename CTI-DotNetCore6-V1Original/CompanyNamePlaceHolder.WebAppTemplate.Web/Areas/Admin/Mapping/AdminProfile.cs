using AutoMapper;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Models;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Mapping;

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
