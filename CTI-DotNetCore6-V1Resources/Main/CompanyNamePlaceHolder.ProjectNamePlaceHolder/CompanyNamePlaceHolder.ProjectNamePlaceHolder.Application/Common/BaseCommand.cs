namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;

public record BaseCommand()
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
