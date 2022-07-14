namespace CompanyNamePlaceHolder.Common.Core.Base.Models;

public record BaseEntity : IEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string? Entity { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
}
