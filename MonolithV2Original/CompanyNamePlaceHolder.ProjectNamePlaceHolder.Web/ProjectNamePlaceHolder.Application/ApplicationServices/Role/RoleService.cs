using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Application.Exception;
using ProjectNamePlaceHolder.Application.Models.Role;
using ProjectNamePlaceHolder.Application.Queries.Role.GetRoleList;
using ProjectNamePlaceHolder.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Application.ApplicationServices.Role
{
    public class RoleService: BaseApplicationService
    {
        public RoleService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {          
        }
              
        public async Task<IList<RoleModel>> GetRoleListAsync()
        {
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new GetRoleListRequest();
            var pagedRoleList = await _mediator.Send(request);
            return await pagedRoleList.ToListAsync();        
        }
    }
}
