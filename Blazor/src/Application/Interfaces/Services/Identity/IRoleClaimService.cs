using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Application.Interfaces.Common;
using ProjectNamePlaceHolder.Application.Requests.Identity;
using ProjectNamePlaceHolder.Application.Responses.Identity;
using ProjectNamePlaceHolder.Shared.Wrapper;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}