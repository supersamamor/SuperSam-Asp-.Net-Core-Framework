using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static LanguageExt.Prelude;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.IdentityExtensions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles
{
    [Authorize(Policy = Permission.Roles.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public RoleViewModel Role { get; set; } = new();

        [BindProperty]
        public IList<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();

        public IActionResult OnGet()
        {
            Permissions = Permission.GenerateAllPermissions()
                                    .Map(p => new PermissionViewModel { Permission = p })
                                    .ToList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var role = await Optional(await _roleManager.FindByNameAsync(Role.Name))
                .MatchAsync(
                    Some: role => Fail<Error, IdentityRole>($"Role with name {role.Name} already exists"),
                    None: async () => await CreateRole(new IdentityRole(Role.Name)).BindT(
                        async r => await AddPermissionsToRole(r)));
            return role.Match<IActionResult>(
                Succ: role =>
                {
                    scope.Complete();
                    Logger.LogInformation("Created Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    return RedirectToPage("View", new { id = role.Id });
                },
                Fail: errors =>
                {
                    Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                    ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                    return Page();
                });
        }

        async Task<Validation<Error, IdentityRole>> CreateRole(IdentityRole role)
        {
            return await TryAsync<Validation<Error, IdentityRole>>(async () =>
            {
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return result.Errors.Select(e => e.Description).Map(e => new Error(e)).ToSeq();
                }
                return role;
            }).IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPost");
                return new Error(ex.Message);
            });
        }

        async Task<Validation<Error, IdentityRole>> AddPermissionsToRole(IdentityRole role)
        {
            var permissions = Permissions.Where(p => p.Enabled).Select(p => p.Permission);
            return await _roleManager.AddPermissionClaims(role, permissions);
        }
    }
}
