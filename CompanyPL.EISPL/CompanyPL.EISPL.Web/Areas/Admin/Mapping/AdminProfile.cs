using AutoMapper;
using CompanyPL.EISPL.Web.Areas.Admin.Commands.Entities;
using CompanyPL.EISPL.Web.Areas.Admin.Models;
using CompanyPL.Common.Data;
using Microsoft.AspNetCore.Identity;
using CompanyPL.EISPL.Core.Identity;

namespace CompanyPL.EISPL.Web.Areas.Admin.Mapping;

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
