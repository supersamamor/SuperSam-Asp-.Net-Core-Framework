
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services
{
    public interface IAuthenticatedUserService
    {
        string? UserId { get; }
        string? Username { get; }
        string? Entity { get; }
    }
}