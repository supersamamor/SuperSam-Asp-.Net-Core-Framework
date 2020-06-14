﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProjectNamePlaceHolder.Web.AppException;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.User;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.ApiServices.User
{
    public class UserAPIService: BaseApiService
    {
        public UserAPIService(HttpClient client, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(client, userManager, httpContext)
        {
        }

        public async Task<IPagedList<UserModel>> GetUserListAsync(string searchKey, string orderBy, string sortBy, int pageIndex,
            int pageSize, CancellationToken token)
        {
            var url = @"";
            url += string.Concat("?", "searchKey=", searchKey, "&&orderBy=", orderBy, "&&sortBy=", sortBy,
                "&&pageIndex=", pageIndex, "&&pageSize=", pageSize);         
            var response = await _client.GetAsync(@"User/" + url, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<CustomPagedList<UserModel>>(result);
            return new StaticPagedList<UserModel>(record.Items, record.PagedListMetaData.PageNumber, record.PagedListMetaData.PageSize, record.PagedListMetaData.TotalItemCount);
        }

        public async Task<UserModel> GetUserItemAsync(int id, CancellationToken token)
        {
            var response = await _client.GetAsync(@"User/" + id.ToString(), token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<UserModel>(result);
            return record;
        }
        public async Task<UserModel> UpdateUserAsync(UserModel user, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"User/", httpContent, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            return JsonConvert.DeserializeObject<UserModel>(result);
        }
        public async Task<UserModel> ActivateUserAsync(UserModel user, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"User/Activate", httpContent, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            return JsonConvert.DeserializeObject<UserModel>(result);
        }
    }
}
