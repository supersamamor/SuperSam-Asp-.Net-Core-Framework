using System.Linq;
using System.Security.Claims;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Authorization
{
    public static class AuthorizationExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permission, string issuer) =>
            user.Claims.Any(c => c.Type == AuthorizationClaimTypes.Permission
                                 && c.Value == permission
                                 && c.Issuer == issuer);
    }
}