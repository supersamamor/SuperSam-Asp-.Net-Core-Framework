using AutoMapper;
using CompanyPL.ProjectPL.Web.Areas.Admin.Commands.Entities;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.Common.Data;
using Microsoft.AspNetCore.Identity;
using CompanyPL.ProjectPL.Core.Identity;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Mapping;

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
