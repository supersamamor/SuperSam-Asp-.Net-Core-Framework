using AutoMapper;
using CTI.DPI.Web.Areas.Admin.Commands.Entities;
using CTI.DPI.Web.Areas.Admin.Models;
using CTI.Common.Data;
using Microsoft.AspNetCore.Identity;
using CTI.DPI.Core.Identity;

namespace CTI.DPI.Web.Areas.Admin.Mapping;

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
