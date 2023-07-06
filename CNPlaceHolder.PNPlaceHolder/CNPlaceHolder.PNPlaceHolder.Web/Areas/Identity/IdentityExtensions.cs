using CNPlaceHolder.Common.Web.Utility.Authorization;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static LanguageExt.Prelude;
using CNPlaceHolder.PNPlaceHolder.Core.Identity;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Identity;

public static class IdentityExtensions
{
    public static Func<T, Task<Validation<Error, T>>> ToValidation<T>(Func<T, Task<IdentityResult>> f) =>
        async param =>
        {
            var result = await f(param);
            return result.Succeeded
                 ? param
                 : Fail<Error, T>(string.Join(",", result.Errors.Select(e => e.Description)));
        };

    public static async Task<Validation<Error, ApplicationRole>> AddPermissionClaimsForModule(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string module)
    {
        var claims = Permission.GeneratePermissionsForModule(module)
                               .Map(p => new Claim(AuthorizationClaimTypes.Permission, p));
        return await roleManager.AddClaims(role, claims);
    }

    public static async Task<Validation<Error, ApplicationRole>> AddPermissionClaims(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, IEnumerable<string> permissions)
    {
        var claims = permissions.Map(p => new Claim(AuthorizationClaimTypes.Permission, p));
        return await roleManager.AddClaims(role, claims);
    }

    public static async Task<Validation<Error, ApplicationRole>> AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string permission)
        => await roleManager.AddClaim(role, new Claim(AuthorizationClaimTypes.Permission, permission));

    public static async Task<Validation<Error, ApplicationRole>> AddClaims(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, IEnumerable<Claim> claims)
    {
        var errors = new Seq<Error>();
        foreach (var claim in claims)
        {
            (await roleManager.AddClaim(role, claim))
                .IfFail(e => { errors = errors.Concat(e); });
        }
        return errors.Count > 0 ? errors : role;
    }

    public static async Task<Validation<Error, ApplicationRole>> AddClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, Claim claim)
    {
        var roleClaims = await roleManager.GetClaimsAsync(role);
        if (!roleClaims.Any(a => a.Type == claim.Type && a.Value == claim.Value))
        {
            var result = await roleManager.AddClaimAsync(role, claim);
            return result.Succeeded
                   ? role!
                   : result.Errors.Select(e => Error.New(e.Description)).ToSeq();
        }
        return role;
    }

    public static async Task<Validation<Error, ApplicationRole>> RemoveAllPermissionClaims(this RoleManager<ApplicationRole> roleManager, ApplicationRole role)
    {
        var errors = new Seq<Error>();
        var claims = await roleManager.GetClaimsAsync(role);
        foreach (var claim in claims.Where(c => c.Type == AuthorizationClaimTypes.Permission))
        {
            var result = await roleManager.RemoveClaimAsync(role, claim);
            if (!result.Succeeded)
            {
                errors = errors.Concat(result.Errors.Select(e => Error.New(e.Description)));
            }
        }
        return errors.Count > 0 ? errors : role;
    }

    public static async Task<Validation<Error, ApplicationUser>> AddRoles(this UserManager<ApplicationUser> userManager, ApplicationUser user, IEnumerable<string> roles)
    {
        var result = await userManager.AddToRolesAsync(user, roles);
        return result.Succeeded
               ? user
               : result.Errors.Select(e => Error.New(e.Description)).ToSeq();
    }

    public static async Task<Validation<Error, ApplicationUser>> RemoveAllRoles(this UserManager<ApplicationUser> userManager, ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var result = await userManager.RemoveFromRolesAsync(user, roles);
        return result.Succeeded
               ? user
               : result.Errors.Select(e => Error.New(e.Description)).ToSeq();
    }
}
