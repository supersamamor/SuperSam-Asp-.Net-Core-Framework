using CompanyNamePlaceHolder.Common.Core.Base.Models;

namespace CompanyNamePlaceHolder.Common.Core.Commands;

public record BaseCommand() : IEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
