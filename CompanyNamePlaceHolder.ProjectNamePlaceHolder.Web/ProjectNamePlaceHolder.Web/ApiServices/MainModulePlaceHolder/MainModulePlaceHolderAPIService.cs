using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using ProjectNamePlaceHolder.Web.AppException;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace ProjectNamePlaceHolder.Web.ApiServices.MainModulePlaceHolder
{
    public class MainModulePlaceHolderAPIService  : BaseApiService
    {
        public MainModulePlaceHolderAPIService(HttpClient client, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext, IConfiguration config) 
            : base(client, userManager, httpContext)
        {   
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.GetValue<string>("ProjectNamePlaceHolderWebConfig:MainModulePlaceHolderToken"));
        }

        public async Task<IPagedList<MainModulePlaceHolderModel>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize, CancellationToken token)
        {
            var url = @"";
            url += string.Concat("?", _userParameter, "&searchKey=", searchKey, "&&orderBy=", orderBy, "&&sortBy=", sortBy,
                "&&pageIndex=", pageIndex, "&&pageSize=", pageSize);
            var response = await _client.GetAsync(@"MainModulePlaceHolder/" + url, token);          
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {            
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<CustomPagedList<MainModulePlaceHolderModel>>(result);
            return new StaticPagedList<MainModulePlaceHolderModel>(record.Items, record.PagedListMetaData.PageNumber, record.PagedListMetaData.PageSize, record.PagedListMetaData.TotalItemCount);
        }

        public async Task<MainModulePlaceHolderModel> GetMainModulePlaceHolderItemAsync(int id, CancellationToken token)
        {
            var response = await _client.GetAsync(@"MainModulePlaceHolder/" + id.ToString() + "?" + _userParameter , token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            var record = JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
            return record;
        }

        public async Task<MainModulePlaceHolderModel> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(mainModulePlaceHolder);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"MainModulePlaceHolder/" + _userParameter, httpContent, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            return JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
        }

        public async Task<MainModulePlaceHolderModel> SaveMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(mainModulePlaceHolder);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(@"MainModulePlaceHolder/" + _userParameter, httpContent, token);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result);
            }
            return JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
        }

        public async Task DeleteMainModulePlaceHolderAsync(int id, CancellationToken token)
        {
            var response = await _client.DeleteAsync(@"MainModulePlaceHolder/" + id.ToString() + "?" + _userParameter, token);     
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {               
                throw new ApiResponseException(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
