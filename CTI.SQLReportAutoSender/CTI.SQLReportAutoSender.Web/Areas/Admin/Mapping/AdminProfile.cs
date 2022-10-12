using AutoMapper;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Commands.Entities;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Models;
using CTI.Common.Data;
using Microsoft.AspNetCore.Identity;
using CTI.SQLReportAutoSender.Core.Identity;

namespace CTI.SQLReportAutoSender.Web.Areas.Admin.Mapping;

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
