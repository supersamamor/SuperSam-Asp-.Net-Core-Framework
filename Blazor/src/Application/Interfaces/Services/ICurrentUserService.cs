using ProjectNamePlaceHolder.Application.Interfaces.Common;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}