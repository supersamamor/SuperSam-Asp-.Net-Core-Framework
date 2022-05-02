using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Authorization
{
    public static class AuthorizationExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permission, string issuer) =>
            user.Claims.Any(c => c.Type == AuthorizationClaimTypes.Permission
                                 && c.Value == permission
                                 && c.Issuer == issuer);
    }
}
