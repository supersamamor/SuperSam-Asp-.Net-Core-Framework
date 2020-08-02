using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ProjectNamePlaceHolder.Web.ApplicationServices
{
    public class BaseApplicationService
    {  
        protected string _userName { get; set; }     
        protected IMediator _mediator;
        public BaseApplicationService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext)
        {           
            _userName = (userManager.GetUserAsync(httpContext.HttpContext.User).Result).UserName;     
            _mediator = mediator;
        }
    }
}
