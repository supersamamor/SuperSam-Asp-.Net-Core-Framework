using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using ProjectNamePlaceHolder.SecurityData.Models;
namespace ProjectNamePlaceHolder.Web.ApiServices.MainModulePlaceHolder
{
    public class BaseApiService
    {
        protected HttpClient _client { get; set; }     
        public BaseApiService(HttpClient client, UserManager<AppUser> userManager, IHttpContextAccessor httpContext)
        {
            _client = client;
            var user = (userManager.GetUserAsync(httpContext.HttpContext.User).Result);
            _client.DefaultRequestHeaders.Add("UserName", user.UserName);
        }
    }
}
