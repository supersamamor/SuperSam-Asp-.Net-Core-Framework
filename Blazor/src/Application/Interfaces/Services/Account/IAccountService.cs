using ProjectNamePlaceHolder.Application.Interfaces.Common;
using ProjectNamePlaceHolder.Application.Requests.Identity;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}