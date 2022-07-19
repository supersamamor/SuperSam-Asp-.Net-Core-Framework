namespace CompanyNamePlaceHolder.Common.Core.Base.Models;

/// <summary>
/// Interface that represents a domain class
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Id of this entity
    /// </summary>
    string Id { get; init; }
}