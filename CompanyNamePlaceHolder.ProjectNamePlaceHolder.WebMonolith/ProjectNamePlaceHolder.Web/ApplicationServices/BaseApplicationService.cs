using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ProjectNamePlaceHolder.Web.ApplicationServices
{
    public class BaseApplicationService
    {  
        protected IdentityUser _user { get; set; }
        protected string _userParameter { get; set; }
        protected IMediator _mediator;
        public BaseApplicationService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext)
        {           
            var claims = httpContext.HttpContext.User;
            _user = (userManager.GetUserAsync(claims).Result);          
            _userParameter = "UserName=" + _user.UserName;
            _mediator = mediator;
        }
    }
}
