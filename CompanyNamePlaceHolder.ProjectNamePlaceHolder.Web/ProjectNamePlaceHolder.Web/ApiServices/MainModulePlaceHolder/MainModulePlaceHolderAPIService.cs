using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using ProjectNamePlaceHolder.Web.AppException;
using X.PagedList;
using ProjectNamePlaceHolder.SecurityData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ProjectNamePlaceHolder.Web.ApiServices.MainModulePlaceHolder
{
    public class MainModulePlaceHolderAPIService  : BaseApiService
    {
        public MainModulePlaceHolderAPIService(HttpClient client, UserManager<AppUser> userManager, IHttpContextAccessor httpContext) 
            : base(client, userManager, httpContext)
        {          
        }

        public async Task<IPagedList<MainModulePlaceHolderModel>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize, CancellationToken token)
        {
            var url = @"";
            url += string.Concat("?", "searchKey=", searchKey, "&&orderBy=", orderBy, "&&sortBy=", sortBy,
                "&&pageIndex=", pageIndex, "&&pageSize=", pageSize);
            var response = await _client.GetAsync(url, token);          
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
            var response = await _client.GetAsync(id.ToString(), token);
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

        public async Task<MainModulePlaceHolderModel> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel template, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(template);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(@"", httpContent, token);
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

        public async Task<MainModulePlaceHolderModel> SaveMainModulePlaceHolderAsync(MainModulePlaceHolderModel template, CancellationToken token)
        {
            var content = JsonConvert.SerializeObject(template);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(@"", httpContent, token);
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
            var response = await _client.DeleteAsync(id.ToString(), token);     
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
