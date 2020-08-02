using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Web.Models.Role;
using ProjectNamePlaceHolder.Web.Queries.Role.GetRoleList;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.ApplicationServices.Role
{
    public class RoleService: BaseApplicationService
    {
        public RoleService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {          
        }

        public async Task<IList<RoleModel>> GetCurrentRoleListAsync(int userId)
        {
            var request = new GetRoleListRequest
            {       
                FilterBy = "CurrentRoles",
                UserId = userId
            };
            var pagedRoleList = await _mediator.Send(request);
            return await pagedRoleList.ToListAsync();
        }

        public async Task<IList<RoleModel>> GetAvailableRoleListAsync(int userId)
        {
            var request = new GetRoleListRequest
            {        
                FilterBy = "AvailableRoles",
                UserId = userId
            };
            var pagedRoleList = await _mediator.Send(request);
            return await pagedRoleList.ToListAsync();        
        }
    }
}
