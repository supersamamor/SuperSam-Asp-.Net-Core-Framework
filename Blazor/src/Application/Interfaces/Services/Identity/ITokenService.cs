using ProjectNamePlaceHolder.Application.Interfaces.Common;
using ProjectNamePlaceHolder.Application.Requests.Identity;
using ProjectNamePlaceHolder.Application.Responses.Identity;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}