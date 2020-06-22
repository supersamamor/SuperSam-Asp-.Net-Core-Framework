using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
namespace ProjectNamePlaceHolder.Web.ApiServices
{
    public class BaseApiService
    {
        protected HttpClient _client { get; set; }
        protected IdentityUser _user { get; set; }     
        public BaseApiService(HttpClient client, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext)
        {
            _client = client;
            var claims = httpContext.HttpContext.User;
            _user = (userManager.GetUserAsync(claims).Result);
            _client.DefaultRequestHeaders.Add("UserName", _user.UserName);       
        }
    }
}
