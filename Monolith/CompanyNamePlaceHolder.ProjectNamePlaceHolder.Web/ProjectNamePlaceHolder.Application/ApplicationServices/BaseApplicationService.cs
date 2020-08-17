using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProjectNamePlaceHolder.Application.ApplicationServices
{
    public class BaseApplicationService
    {  
        protected string _userName { get; set; }     
        protected IMediator _mediator { get; set; }
        protected ClaimsPrincipal _claims { get; set; }
        public BaseApplicationService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext)
        {           
            _userName = (userManager.GetUserAsync(httpContext.HttpContext.User).Result).UserName;     
            _mediator = mediator;
            _claims = httpContext.HttpContext.User;
        }
    }
}
