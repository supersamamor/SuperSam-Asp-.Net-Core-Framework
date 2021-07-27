using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles.Manage
{
    [Authorize(Policy = Permission.Roles.Edit)]
    public class EditPermissionsModel : BasePageModel<EditPermissionsModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditPermissionsModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        [BindProperty]
        public IList<PermissionViewModel>? Permissions { get; set; }

        [BindProperty]
        public RoleViewModel? Role { get; set; }

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
                                    return await GetPermisionsForRole(role);
                                }, none: null);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Role!.Id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return await Mediatr.Send(new GetRoleByIdQuery(Role.Id))
                                .ToActionResult(async role => await UpdatePermissionsForRole(role), none: null);
        }

        async Task<IActionResult> GetPermisionsForRole(IdentityRole role)
        {
            var roleClaims = (await _roleManager.GetClaimsAsync(role)).Map(c => c.Value);
            Permissions = Permission.GenerateAllPermissions().Map(p => new PermissionViewModel
            {
                Permission = p,
                Enabled = roleClaims.Any(c => c == p)
            }).ToList();
            return Page();
        }

        async Task<IActionResult> UpdatePermissionsForRole(IdentityRole role)
        {
            var result = await _roleManager.RemoveAllPermissionClaims(role);
            return await result.MatchAsync(
                async role => await AddPermissionsToRole(role),
                errors =>
                {
                    ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                    Logger.LogError("Error in OnPostAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<IActionResult> AddPermissionsToRole(IdentityRole role)
        {
            return await Optional(Permissions!.Where(p => p.Enabled).Select(p => p.Permission))
                .MatchAsync(
                async permissions =>
                {
                    var updatedRole = await _roleManager.AddPermissionClaims(role, permissions!);
                    return updatedRole.Match<IActionResult>(
                    Succ: role =>
                    {
                        NotyfService.Success(Localizer["Record saved successfully"]);
                        Logger.LogInformation("Updated Permissions for Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                        return Redirect($"/Admin/Roles/{Id}/Permissions");
                    },
                    Fail: errors =>
                    {
                        ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                        Logger.LogError("Error in OnPostAsync. Error: {Errors}", string.Join(",", errors));
                        return Page();
                    });
                },
                () =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Updated Permissions for Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                    return Redirect($"/Admin/Roles/{Id}/Permissions");
                });
        }
    }
}
