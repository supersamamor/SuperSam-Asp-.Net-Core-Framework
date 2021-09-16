using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(Claims.Subject) == null ? "" : httpContextAccessor.HttpContext!.User!.FindFirst(Claims.Subject)!.Value;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(Claims.Name) == null ? "" : httpContextAccessor.HttpContext!.User!.FindFirst(Claims.Name)!.Value;
            Entity = httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Entity) == null ? "" : httpContextAccessor.HttpContext!.User!.FindFirst(CustomClaimTypes.Entity)!.Value;
        }

        public string UserId { get; }
        public string Username { get; }
        public string Entity { get; }
    }
}
