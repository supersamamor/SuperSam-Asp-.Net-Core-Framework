namespace CNPlaceHolder.Common.Identity.Abstractions;

/// <summary>
/// Represents an authenticated user
/// </summary>
public interface IAuthenticatedUser
{
    /// <summary>
    /// Represents the tenant that this user belongs to. Used for multi-tenant support.
    /// </summary>
    string? Entity { get; }

    /// <summary>
    /// Unique identifier for the current request.
    /// </summary>
    string? TraceId { get; }

    /// <summary>
    /// Id of this user.
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Username of this user.
    /// </summary>
    string? Username { get; }
	public System.Security.Claims.ClaimsPrincipal? ClaimsPrincipal { get; }
}