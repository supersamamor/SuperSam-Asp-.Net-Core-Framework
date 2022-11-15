using CTI.Common.Core.Base.Models;

namespace CTI.Common.Core.Commands;

/// <summary>
/// A base class for command classes.
/// </summary>
public abstract record BaseCommand() : IEntity
{
    /// <summary>
    /// The default primary key
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
