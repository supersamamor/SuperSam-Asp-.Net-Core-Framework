using Microsoft.AspNetCore.Authorization;

namespace CTI.Common.Web.Utility.Authorization;

/// <summary>
/// The permission authorization requirement.
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// The name of the permission.
    /// </summary>
    public string Permission { get; }

    /// <summary>
    /// Initializes an instance of <see cref="PermissionRequirement"/> with the required permission.
    /// </summary>
    /// <param name="permission">The required permission</param>
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}