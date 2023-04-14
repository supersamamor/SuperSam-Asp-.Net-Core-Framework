using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Areas.Admin.Models;
using CelerSoft.TurboERP.Web.Areas.Admin.Queries.Roles;
using CelerSoft.TurboERP.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static CelerSoft.TurboERP.Web.Areas.Identity.IdentityExtensions;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Pages.Roles;

[Authorize(Policy = Permission.Roles.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public EditModel(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [BindProperty]
    public RoleViewModel Role { get; set; } = new();

    [BindProperty]
    public IList<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetRoleByIdQuery(id))
                            .ToActionResult(async e =>
                            {
                                Role = Mapper.Map<RoleViewModel>(e);
                                Permissions = await GetPermisionsForRole(e);
                                return Page();
                            }, none: null);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Mediatr.Send(new GetRoleByIdQuery(Role.Id)).ToActionResult(
            async r =>
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                return await UpdateRoleFromModel(r)
                .BindT(async r => await UpdatePermissionsForRole(r))
                .ToActionResult(
                    success: role =>
                    {
                        scope.Complete();
                        Logger.LogInformation("Updated Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                        NotyfService.Success(Localizer["Record saved successfully"]);
                        return RedirectToPage("View", new { id = role.Id });
                    },
                    fail: errors =>
                    {
                        Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                        ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                        return Page();
                    });
            },
            none: null);
    }

    async Task<IList<PermissionViewModel>> GetPermisionsForRole(IdentityRole role)
    {
        var roleClaims = (await _roleManager.GetClaimsAsync(role)).Map(c => c.Value);
        return Permission.GenerateAllPermissions().Map(p => new PermissionViewModel
        {
            Permission = p,
            Enabled = roleClaims.Any(c => c == p)
        }).ToList();
    }

    async Task<Validation<Error, IdentityRole>> UpdateRoleFromModel(IdentityRole roleToUpdate)
    {
        roleToUpdate.Name = Role.Name;
        roleToUpdate.NormalizedName = Role.Name.ToUpper();
        return await UpdateRole(roleToUpdate);
    }

    async Task<Validation<Error, IdentityRole>> UpdatePermissionsForRole(IdentityRole role) =>
        await _roleManager.RemoveAllPermissionClaims(role)
                          .BindT(async r => await AddPermissionsToRole(r));

    async Task<Validation<Error, IdentityRole>> AddPermissionsToRole(IdentityRole role)
    {
        var permissions = Permissions.Where(p => p.Enabled).Select(p => p.Permission);
        return await _roleManager.AddPermissionClaims(role, permissions);
    }

    Func<IdentityRole, Task<Validation<Error, IdentityRole>>> UpdateRole =>
        ToValidation<IdentityRole>(_roleManager.UpdateAsync);
}
