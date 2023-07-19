namespace CompanyPL.EISPL.Web.Models;

public record BaseViewModel
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
