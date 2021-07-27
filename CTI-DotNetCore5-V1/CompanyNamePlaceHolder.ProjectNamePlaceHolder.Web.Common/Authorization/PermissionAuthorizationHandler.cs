using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        readonly IConfiguration _configuration;

        public PermissionAuthorizationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            var issuer = _configuration.GetValue<string>("Authentication:Issuer") ?? "LOCAL AUTHORITY";
            var isAllowed = context.User.Claims.Any(c => c.Type == AuthorizationClaimTypes.Permission
                                                         && c.Value == requirement.Permission
                                                         && c.Issuer == issuer);
            if (isAllowed)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
