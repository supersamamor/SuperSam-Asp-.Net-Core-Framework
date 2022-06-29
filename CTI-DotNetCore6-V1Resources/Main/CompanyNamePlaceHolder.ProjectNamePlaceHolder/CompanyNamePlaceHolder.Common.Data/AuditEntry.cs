using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace CompanyNamePlaceHolder.Common.Data;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }

    public EntityEntry Entry { get; }
    public string? UserId { get; set; }
    public string? TraceId { get; set; }
    public string? TableName { get; set; }
    public Dictionary<string, object?> KeyValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> OldValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> NewValues { get; } = new Dictionary<string, object?>();
    public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
    public AuditType? AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public bool HasTemporaryProperties => TemporaryProperties.Any();

    public Audit ToAudit()
    {
        var audit = new Audit
        {
            UserId = UserId,
            TraceId = TraceId,
            Type = AuditType?.ToString(),
            TableName = TableName,
            DateTime = DateTime.UtcNow,
            PrimaryKey = JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
        };
        return audit;
    }
}
