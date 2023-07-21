using System.Security.Claims;

namespace CompanyPL.Common.Web.Utility.Authorization;

/// <summary>
/// Extension methods related to authorization.
/// </summary>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Check if <see cref="ClaimsPrincipal"/> has the specified <paramref name="permission"/> from the specified <paramref name="issuer"/>.
    /// </summary>
    /// <param name="user">Instance of <see cref="ClaimsPrincipal"/></param>
    /// <param name="permission">The permission to check</param>
    /// <param name="issuer">The issuer of the claim</param>
    /// <returns>True if <see cref="ClaimsPrincipal"/> has <paramref name="permission"/> from <paramref name="issuer"/>, False otherwise</returns>
    public static bool HasPermission(this ClaimsPrincipal user, string permission, string issuer) =>
        user.Claims.Any(c => c.Type == AuthorizationClaimTypes.Permission
                             && c.Value == permission
                             && c.Issuer == issuer);
}