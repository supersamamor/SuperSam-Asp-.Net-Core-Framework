using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("sub") == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            Entity = httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Entity) == null ? null : httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Entity)?.Value;
        }

        public string? UserId { get; }
        public string? Username { get; }
        public string? Entity { get; }
    }
}
