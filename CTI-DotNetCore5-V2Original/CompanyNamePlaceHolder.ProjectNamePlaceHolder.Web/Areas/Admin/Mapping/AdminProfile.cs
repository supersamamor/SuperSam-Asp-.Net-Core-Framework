using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Mapping
{
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
}
