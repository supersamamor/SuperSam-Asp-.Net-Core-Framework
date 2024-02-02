namespace CompanyPL.ProjectPL.Application.DTOs
{
    public record class BaseDto
    {
        public string Id { get; init; } = "";
        public DateTime LastModifiedDate { get; set; }
    }
}
