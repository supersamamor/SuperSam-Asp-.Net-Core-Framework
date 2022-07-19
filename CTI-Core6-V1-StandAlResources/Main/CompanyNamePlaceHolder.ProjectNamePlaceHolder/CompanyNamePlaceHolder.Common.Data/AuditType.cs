namespace CompanyNamePlaceHolder.Common.Data;

/// <summary>
/// Transaction types used for audit logging.
/// </summary>
public enum AuditType
{
    /// <summary>
    /// Catch-all
    /// </summary>
    None = 0,

    /// <summary>
    /// Add a record
    /// </summary>
    Create = 1,

    /// <summary>
    /// Edit a record
    /// </summary>
    Update = 2,

    /// <summary>
    /// Delete a record
    /// </summary>
    Delete = 3
}
