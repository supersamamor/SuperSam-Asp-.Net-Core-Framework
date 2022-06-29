using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Identity
{
    public class DefaultAuthenticatedUser : IAuthenticatedUser
    {
        public DefaultAuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User.FindFirst(Claims.Subject)?.Value;
            Username = httpContextAccessor.HttpContext?.User.FindFirst(Claims.Name)?.Value;
            Entity = httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Entity)?.Value;
            TraceId = Activity.Current?.Id ?? httpContextAccessor.HttpContext?.TraceIdentifier;
        }

        public string? UserId { get; }
        public string? Username { get; }
        public string? Entity { get; }
        public string? TraceId { get; }
    }
}