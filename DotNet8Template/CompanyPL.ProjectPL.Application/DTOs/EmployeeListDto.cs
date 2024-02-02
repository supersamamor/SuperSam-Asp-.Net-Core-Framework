using CompanyPL.Common.Core.Base.Models;
namespace CompanyPL.ProjectPL.Application.DTOs
{
    public record EmployeeListDto : BaseDto
    {
        public string FirstName { get; init; } = "";
        public string EmployeeCode { get; init; } = "";
        public string LastName { get; init; } = "";
        public string MiddleName { get; init; } = "";
        public string StatusBadge { get; set; } = "";
    }
}
