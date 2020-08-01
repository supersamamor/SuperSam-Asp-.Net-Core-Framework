using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProjectNamePlaceHolder.Web.Models.Role;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectNamePlaceHolder.Web.ApplicationServices.Role
{
    public class RoleAPIService: BaseApplicationService
    {
        public RoleAPIService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext, IConfiguration config) 
            : base(userManager, httpContext)
        {          
        }

        public async Task<IList<RoleModel>> GetCurrentRoleListAsync(int userId, CancellationToken token)
        {
            //var url = @"";
            //url += string.Concat("?", _userParameter, "&userId=", userId);         
            //var response = await _client.GetAsync(@"Role/CurrentRoles/" + url, token);
            //var result = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{
            //    throw new ApiResponseException(result);
            //}
            //var record = JsonConvert.DeserializeObject<CustomPagedList<RoleModel>>(result);
            //return record.Items;
            return null;
        }

        public async Task<IList<RoleModel>> GetAvailableRoleListAsync(int userId, CancellationToken token)
        {
            //var url = @"";
            //url += string.Concat("?", _userParameter, "&userId=", userId);
            //var response = await _client.GetAsync(@"Role/AvailableRoles/" + url, token);
            //var result = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{
            //    throw new ApiResponseException(result);
            //}
            //var record = JsonConvert.DeserializeObject<CustomPagedList<RoleModel>>(result);
            //return record.Items;
            return null;
        }
    }
}
