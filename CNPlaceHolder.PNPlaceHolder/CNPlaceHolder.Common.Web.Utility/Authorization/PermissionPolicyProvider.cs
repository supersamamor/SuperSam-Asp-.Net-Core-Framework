using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CNPlaceHolder.Common.Web.Utility.Authorization;

/// <summary>
/// The custom <see cref="AuthorizationPolicy"/> provider.
/// </summary>
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    /// <summary>
    /// The default policy provider.
    /// </summary>
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    /// <summary>
    /// Initializes an instance of <see cref="PermissionPolicyProvider"/>.
    /// </summary>
    /// <param name="options"></param>
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        // There can only be one policy provider in ASP.NET Core.
        // We only handle permissions related policies, for the rest
        // we will use the default provider.
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    /// <summary>
    /// Gets the default policy.
    /// </summary>
    /// <returns></returns>
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    /// <summary>
    /// Gets <see cref="AuthorizationPolicy"/> for the given <paramref name="policyName"/>.
    /// If <paramref name="policyName"/> starts with Permission, creates a policy with a
    /// <see cref="PermissionRequirement"/>. The policy name must match the permission
    /// that is needed.
    /// </summary>
    /// <param name="policyName">The name of the policy</param>
    /// <returns></returns>
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build())!;
        }

        // Policy is not for permissions, try the default provider.
        return FallbackPolicyProvider.GetPolicyAsync(policyName)!;
    }

    /// <summary>
    /// Gets the fallback authorization policy.
    /// </summary>
    /// <returns></returns>
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync()!;
}