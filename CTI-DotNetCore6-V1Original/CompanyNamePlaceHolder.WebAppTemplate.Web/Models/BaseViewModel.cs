namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Models;

public record BaseViewModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
