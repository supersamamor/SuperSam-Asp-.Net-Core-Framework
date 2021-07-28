using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;
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
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles.Manage
{
    [Authorize(Policy = Permission.Roles.Edit)]
    public class EditRoleModel : BasePageModel<EditRoleModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public RoleViewModel? Role { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetRoleByIdQuery(Id))
                                .ToActionResult(e =>
                                {
                                    Role = Mapper.Map<RoleViewModel>(e);
                                    return Page();
                                }, none: null);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Role!.Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetRoleByIdQuery(Role.Id))
                                .ToActionResult(async r => await UpdateRoleFromModel(r), none: null);
        }

        async Task<IActionResult> UpdateRoleFromModel(IdentityRole roleToUpdate)
        {
            roleToUpdate.Name = Role!.Name;
            roleToUpdate.NormalizedName = Role!.Name!.ToUpper();
            return (await UpdateRole(roleToUpdate)).Match<IActionResult>(
                Succ: role =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Updated Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                    return Redirect($"/Admin/Roles/{Id}");
                },
                Fail: errors =>
                {
                    ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                    Logger.LogError("Error in OnPostAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        Func<IdentityRole, Task<Validation<Error, IdentityRole>>> UpdateRole =>
            ToValidation<IdentityRole>(_roleManager.UpdateAsync);
    }
}
