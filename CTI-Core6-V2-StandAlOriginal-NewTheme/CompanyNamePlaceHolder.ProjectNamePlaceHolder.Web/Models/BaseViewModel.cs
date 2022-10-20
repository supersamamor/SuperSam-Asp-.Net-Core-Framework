namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;

public record BaseViewModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
