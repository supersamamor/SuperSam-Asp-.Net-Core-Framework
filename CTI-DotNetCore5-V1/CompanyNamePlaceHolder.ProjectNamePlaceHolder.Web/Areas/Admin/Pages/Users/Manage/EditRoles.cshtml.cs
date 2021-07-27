using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
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
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users.Manage
{
    [Authorize(Policy = Permission.Users.Edit)]
    public class EditRolesModel : BasePageModel<EditRolesModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditRolesModel(RoleManager<IdentityRole> roleManager,
                              UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        [BindProperty]
        public EditRolesUserViewModel? UserModel { get; set; }

        [BindProperty]
        public IList<EditRolesViewModel>? Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetUserByIdQuery(Id))
                                .ToActionResult(async user =>
                                {
                                    UserModel = new() { Id = user.Id, Name = user.Name };
                                    return await GetRolesForUser(user);
                                }, none: null);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserModel!.Id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return await Mediatr.Send(new GetUserByIdQuery(UserModel.Id))
                                .ToActionResult(async user => await UpdateRolesForUser(user), none: null);
        }

        async Task<IActionResult> GetRolesForUser(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            Roles = _roleManager.Roles.Map(r => new EditRolesViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Selected = userRoles.Any(c => c == r.Name)
            }).ToList();
            return Page();
        }

        async Task<IActionResult> UpdateRolesForUser(ApplicationUser user)
        {
            var result = await _userManager.RemoveAllRoles(user);
            return await result.MatchAsync(
                async user => await AddRolesToUser(user),
                errors =>
                {
                    ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                    Logger.LogError("Error in OnPostAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<IActionResult> AddRolesToUser(ApplicationUser user)
        {
            return await Optional(Roles!.Where(r => r.Selected).Select(r => r.Name))
                .MatchAsync(
                async roles =>
                {
                    var updatedUser = await _userManager.AddRoles(user, roles!);
                    return updatedUser.Match<IActionResult>(
                    Succ: user =>
                    {
                        NotyfService.Success(Localizer["Record saved successfully"]);
                        Logger.LogInformation("Updated Roles for User. ID: {ID}, User: {User}", user.Id, user.ToString());
                        return Redirect($"/Admin/Users/{Id}/Roles");
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
                    Logger.LogInformation("Updated Roles for User. ID: {ID}, User: {User}", user.Id, user.ToString());
                    return Redirect($"/Admin/Users/{Id}/Roles");
                });
        }
    }

    public record EditRolesUserViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }

    public record EditRolesViewModel
    {
        public string? Id { get; set; }
        [Display(Name = "Role")]
        public string? Name { get; set; }
        [Display(Name = "Status")]
        public bool Selected { get; set; }
    }
}
