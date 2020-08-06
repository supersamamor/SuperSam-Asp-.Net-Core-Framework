using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Application.Commands.User.ActivateUser;
using ProjectNamePlaceHolder.Application.Commands.User.UpdateUser;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Application.Queries.User.GetUserItem;
using ProjectNamePlaceHolder.Application.Queries.User.GetUserList;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Application.ApplicationServices.User
{
    public class UserService: BaseApplicationService
    {
        public UserService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext) {}

        public async Task<IPagedList<UserModel>> GetUserListAsync(string searchKey, string orderBy, string sortBy, int pageIndex,
            int pageSize)
        {
            var request = new GetUserListRequest
            {
                SearchKey = searchKey,
                OrderBy = orderBy,
                SortBy = sortBy,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return await _mediator.Send(request);   
        }

        public async Task<UserModel> GetUserItemAsync(int id)
        {
            var request = new GetUserItemRequest
            {
                Id = id
            };
            return await _mediator.Send(request);
        }

        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            var request = new UpdateUserRequest
            {
                User = user,
                Username = _userName
            };
            return await _mediator.Send(request);           
        }

        public async Task<UserModel> ActivateUserAsync(int id)
        {                   
            var request = new ActivateUserRequest
            {
                Id = id,
                Username = _userName
            };
            return await _mediator.Send(request);
        }
    }
}
