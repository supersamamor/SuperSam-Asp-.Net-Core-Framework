using CelerSoft.Common.Identity.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CelerSoft.Common.Web.Utility.Identity;

/// <summary>
/// Default implmentation of <see cref="IAuthenticatedUser"/>.
/// </summary>
public class DefaultAuthenticatedUser : IAuthenticatedUser
{
    /// <summary>
    /// Initializes an instance of <see cref="DefaultAuthenticatedUser"/>.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public DefaultAuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirst(Claims.Subject)?.Value;
        Username = httpContextAccessor.HttpContext?.User.FindFirst(Claims.Name)?.Value;
        Entity = httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Entity)?.Value;
        TraceId = Activity.Current?.Id ?? httpContextAccessor.HttpContext?.TraceIdentifier;
    }

    /// <summary>
    /// Id of this user.
    /// </summary>
    public string? UserId { get; }

    /// <summary>
    /// Username of this user.
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Represents the tenant that this user belongs to. Used for multi-tenant support.
    /// </summary>
    public string? Entity { get; }

    /// <summary>
    /// Unique identifier for the current request.
    /// </summary>
    public string? TraceId { get; }
}