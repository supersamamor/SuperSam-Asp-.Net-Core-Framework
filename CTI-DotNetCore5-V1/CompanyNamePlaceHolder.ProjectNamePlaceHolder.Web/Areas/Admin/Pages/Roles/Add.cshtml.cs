using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;

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
        public RoleViewModel? Role { get; set; }

        public IActionResult OnGetAdd()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var role = await Optional(await _roleManager.FindByNameAsync(Role!.Name!))
                .MatchAsync(
                    Some: role => Fail<Error, IdentityRole>($"Role with name {role.Name} already exists"),
                    None: async () => await CreateRole(new IdentityRole(Role!.Name)));
            return role.Match<IActionResult>(
                Succ: role =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Created Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                    return RedirectToPage("Manage/Roles", new { id = role.Id });
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<Validation<Error, IdentityRole>> CreateRole(IdentityRole role)
        {
            return await TryAsync(async () =>
            {
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", result.Errors.Select(e => e.Description)));
                    return Fail<Error, IdentityRole>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                }
                return role;
            }).IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPostAddAsync");
                return Fail<Error, IdentityRole>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            });
        }
    }
}
