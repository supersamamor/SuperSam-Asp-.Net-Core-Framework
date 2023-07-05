using CNPlaceHolder.Common.Core.Base.Models;

namespace CNPlaceHolder.Common.Core.Commands;

/// <summary>
/// A base class for command classes.
/// </summary>
public abstract record BaseCommand() : IEntity
{
    /// <summary>
    /// The default primary key
    /// </summary>
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
