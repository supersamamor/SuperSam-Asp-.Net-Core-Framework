﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProjectNamePlaceHolder.Web.AppException;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.Role;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.ApiServices.Role
{
    public class RoleAPIService: BaseApiService
    {
        public RoleAPIService(HttpClient client, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(client, userManager, httpContext)
        {
        }

        public async Task<IList<RoleModel>> GetCurrentRoleListAsync(int userId, CancellationToken token)
        {
            var url = @"";
            url += string.Concat("?", "userId=", userId);         
            var response = await _client.GetAsync(@"Role/CurrentRoles/" + url, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<CustomPagedList<RoleModel>>(result);
            return record.Items;
        }

        public async Task<IList<RoleModel>> GetAvailableRoleListAsync(int userId, CancellationToken token)
        {
            var url = @"";
            url += string.Concat("?", "userId=", userId);
            var response = await _client.GetAsync(@"Role/AvailableRoles/" + url, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<CustomPagedList<RoleModel>>(result);
            return record.Items;
        }
    }
}
