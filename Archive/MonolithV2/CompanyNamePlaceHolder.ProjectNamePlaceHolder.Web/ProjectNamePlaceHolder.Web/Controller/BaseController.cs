using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNamePlaceHolder.Web.Filters;
namespace ProjectNamePlaceHolder.Web.Controller
{
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    [AuthorizeApiKey]
    public class BaseController : ControllerBase
    {      
    }
}
