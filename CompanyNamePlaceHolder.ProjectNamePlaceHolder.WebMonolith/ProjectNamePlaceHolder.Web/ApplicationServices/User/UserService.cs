using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Web.Commands.User.ActivateUser;
using ProjectNamePlaceHolder.Web.Commands.User.UpdateUser;
using ProjectNamePlaceHolder.Web.Models.User;
using ProjectNamePlaceHolder.Web.Queries.User.GetUserItem;
using ProjectNamePlaceHolder.Web.Queries.User.GetUserList;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.ApplicationServices.User
{
    public class UserService: BaseApplicationService
    {
        public UserService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {          
        }

        public async Task<IPagedList<UserModel>> GetUserListAsync(string searchKey, string orderBy, string sortBy, int pageIndex,
            int pageSize, CancellationToken token)
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
