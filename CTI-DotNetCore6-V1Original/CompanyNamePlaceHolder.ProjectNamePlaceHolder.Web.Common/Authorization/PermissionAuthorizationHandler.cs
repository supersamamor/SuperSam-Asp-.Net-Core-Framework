using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
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
            var isAllowed = context.User.HasPermission(requirement.Permission, issuer)
                            || context.User.HasScope(requirement.Permission);
            if (isAllowed)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
