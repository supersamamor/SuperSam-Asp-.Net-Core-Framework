using Template.Web.Models;
using Template.Web.Models.Template;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Template.Web.AppException;
using X.PagedList;
using Template.SecurityData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Template.Web.ApiServices.Template
{
    public class TemplateAPIService  : BaseApiService
    {
        public TemplateAPIService(HttpClient client, UserManager<AppUser> userManager, IHttpContextAccessor httpContext) 
            : base(client, userManager, httpContext)
        {          
        }

        public async Task<IPagedList<TemplateModel>> GetTemplateListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize, CancellationToken token)
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
            var record = JsonConvert.DeserializeObject<CustomPagedList<TemplateModel>>(result);
            return new StaticPagedList<TemplateModel>(record.Items, record.PagedListMetaData.PageNumber, record.PagedListMetaData.PageSize, record.PagedListMetaData.TotalItemCount);
        }

        public async Task<TemplateModel> GetTemplateItemAsync(int id, CancellationToken token)
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
            var record = JsonConvert.DeserializeObject<TemplateModel>(result);
            return record;
        }

        public async Task<TemplateModel> UpdateTemplateAsync(TemplateModel template, CancellationToken token)
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
            return JsonConvert.DeserializeObject<TemplateModel>(result);
        }

        public async Task<TemplateModel> SaveTemplateAsync(TemplateModel template, CancellationToken token)
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
            return JsonConvert.DeserializeObject<TemplateModel>(result);
        }

        public async Task DeleteTemplateAsync(int id, CancellationToken token)
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
