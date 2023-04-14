using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;

namespace CelerSoft.Common.Web.Utility.Authorization;

/// <summary>
/// Authorization handler for <see cref="PermissionRequirement"/>.
/// </summary>
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes an instance of <see cref="PermissionAuthorizationHandler"/>.
    /// </summary>
    /// <param name="configuration"></param>
    public PermissionAuthorizationHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Authorize if the user has the specified <see cref="PermissionRequirement"/>.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement">The required permission</param>
    /// <returns></returns>
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