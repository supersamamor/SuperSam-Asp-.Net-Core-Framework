namespace CNPlaceHolder.Common.Data;

/// <summary>
/// A class representing an audit log record in the database.
/// </summary>
public class Audit
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the user related to this audit log.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The type of transaction, e.g. Add, Edit, Delete, etc.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The table affected.
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// Timestamp of the transaction.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Values of the fields before the transaction.
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// Values of the fields after the transaction.
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// The columns affected.
    /// </summary>
    public string? AffectedColumns { get; set; }

    /// <summary>
    /// Primary key of this table.
    /// </summary>
    public string? PrimaryKey { get; set; }

    /// <summary>
    /// Unique identifier for the current request.
    /// </summary>
    public string? TraceId { get; set; }
}
