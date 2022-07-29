namespace CTI.Common.Core.Base.Models;

/// <summary>
/// A base class for domain classes
/// </summary>
public abstract record BaseEntity : IEntity
{
    /// <summary>
    /// Id of this entity
    /// </summary>
    public string Id { get; init; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Field representing the tenant who owns this entity. Used for multi-tenant support.
    /// </summary>
    public string? Entity { get; set; }

    /// <summary>
    /// User id who created this entity
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Timestamp when this entity was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Id of the user who last modified this entity
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Timestamp when the entity was last modified
    /// </summary>
    public DateTime LastModifiedDate { get; set; }
}
