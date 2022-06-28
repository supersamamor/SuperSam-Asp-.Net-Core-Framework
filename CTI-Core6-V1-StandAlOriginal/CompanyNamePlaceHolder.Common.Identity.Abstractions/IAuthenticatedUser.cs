namespace CompanyNamePlaceHolder.Common.Identity.Abstractions;

/// <summary>
/// Represents an authenticated user
/// </summary>
public interface IAuthenticatedUser
{
    /// <summary>
    /// Represents the tenant that this user belongs to. Used for multi-tenancy.
    /// </summary>
    string? Entity { get; }

    /// <summary>
    /// Unique identifier for this request
    /// </summary>
    string? TraceId { get; }

    /// <summary>
    /// Id of this user
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Username
    /// </summary>
    string? Username { get; }
}