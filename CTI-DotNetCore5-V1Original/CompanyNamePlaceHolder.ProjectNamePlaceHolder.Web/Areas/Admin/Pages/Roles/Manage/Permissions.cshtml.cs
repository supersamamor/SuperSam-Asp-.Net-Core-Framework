using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles.Manage
{
    [Authorize(Policy = Permission.Roles.View)]
    public class PermissionsModel : BasePageModel<PermissionsModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionsModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        [BindProperty]
        public RoleViewModel? Role { get; set; }

        public IList<PermissionViewModel>? Permissions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetRoleByIdQuery(Id))
                                .ToActionResult(async role =>
                                {
                                    Role = Mapper.Map<RoleViewModel>(role);
                                    var roleClaims = (await _roleManager.GetClaimsAsync(role)).Map(c => c.Value);
                                    Permissions = Permission.GenerateAllPermissions().Map(p => new PermissionViewModel
                                    {
                                        Permission = p,
                                        Enabled = roleClaims.Any(c => c == p)
                                    }).ToList();
                                    return Page();
                                }, none: null);
        }
    }
}
