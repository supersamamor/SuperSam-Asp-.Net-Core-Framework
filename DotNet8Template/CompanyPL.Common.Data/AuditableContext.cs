using Microsoft.EntityFrameworkCore;

namespace CompanyPL.Common.Data;

/// <summary>
/// A base class for contexts that automatically creates audit logs for transactions.
/// </summary>
public abstract class AuditableContext : DbContext
{
    /// <summary>
    /// Creates an instance of <see cref="AuditableContext"/>
    /// </summary>
    /// <param name="options"></param>
    public AuditableContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// The audit logs table.
    /// </summary>
    public DbSet<Audit> AuditLogs { get; set; } = default!;

    /// <summary>
    /// Create audit logs records based on the entities added/edited/deleted.
    /// </summary>
    /// <param name="userId">The id of the user doing the transaction</param>
    /// <param name="traceId">Unique identifier for this transaction</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<int> SaveChangesAsync(string? userId = null, string? traceId = null, CancellationToken cancellationToken = default)
	{
		var auditEntries = await OnBeforeSaveChanges(userId, traceId);
		var result = await base.SaveChangesAsync(cancellationToken);
		await OnAfterSaveChanges(auditEntries);
		return result;
	}

	private async Task<List<AuditEntry>> OnBeforeSaveChanges(string? userId, string? traceId)
	{
		ChangeTracker.DetectChanges();
		var auditEntries = new List<AuditEntry>();

		foreach (var entry in ChangeTracker.Entries())
		{
			if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
				continue;

			var auditEntry = new AuditEntry(entry)
			{
				TableName = entry.Entity.GetType().Name,
				UserId = userId,
				TraceId = traceId
			};
			auditEntries.Add(auditEntry);

			foreach (var property in entry.Properties)
			{
				if (property.IsTemporary)
				{
					auditEntry.TemporaryProperties.Add(property);
					continue;
				}

				var propertyName = property.Metadata.Name;
				if (property.Metadata.IsPrimaryKey())
				{
					auditEntry.KeyValues[propertyName] = property.CurrentValue;
					continue;
				}

				switch (entry.State)
				{
					case EntityState.Added:
						auditEntry.AuditType = AuditType.Create;
						auditEntry.NewValues[propertyName] = property.CurrentValue;
						break;

					case EntityState.Deleted:
						auditEntry.AuditType = AuditType.Delete;
						auditEntry.OldValues[propertyName] = property.OriginalValue;
						break;

					case EntityState.Modified:
						if (property.IsModified)
						{
							auditEntry.ChangedColumns.Add(propertyName);
							auditEntry.AuditType = AuditType.Update;
							auditEntry.OldValues[propertyName] = property.OriginalValue;
							auditEntry.NewValues[propertyName] = property.CurrentValue;
						}
						break;
				}
			}
		}
		// Use asynchronous AddRange to add audit entries to the context
		await AuditLogs.AddRangeAsync(auditEntries.Where(_ => !_.HasTemporaryProperties).Select(auditEntry => auditEntry.ToAudit()));
		return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
	}

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        if (auditEntries == null || auditEntries.Count == 0)
            return Task.CompletedTask;

        foreach (var auditEntry in auditEntries)
        {
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                }
                else
                {
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }
            }
            AuditLogs.Add(auditEntry.ToAudit());
        }
        return SaveChangesAsync();
    }
}
