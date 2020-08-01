using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MediatR;
using ProjectNamePlaceHolder.Web.Queries.GetMainModulePlaceHolderList;

namespace ProjectNamePlaceHolder.Web.ApplicationServices.MainModulePlaceHolder
{
    public class MainModulePlaceHolderService  : BaseApplicationService
    {
        public MainModulePlaceHolderService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {            
        }

        public async Task<IPagedList<MainModulePlaceHolderModel>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize, CancellationToken token)
        {
            var request = new GetMainModulePlaceHolderListRequest
            {
                SearchKey = searchKey,
                OrderBy = orderBy,
                SortBy = sortBy,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return await _mediator.Send(request);          
        }

        public async Task<MainModulePlaceHolderModel> GetMainModulePlaceHolderItemAsync(int id, CancellationToken token)
        {
            //var response = await _client.GetAsync(@"MainModulePlaceHolder/" + id.ToString() + "?" + _userParameter , token);
            //var result = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{
            //    throw new ApiResponseException(result);
            //}
            //var record = JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
            //return record;
            return null;
        }

        public async Task<MainModulePlaceHolderModel> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder, CancellationToken token)
        {
            //var content = JsonConvert.SerializeObject(mainModulePlaceHolder);
            //var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            //var response = await _client.PutAsync(@"MainModulePlaceHolder?" + _userParameter, httpContent, token);
            //var result = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{
            //    throw new ApiResponseException(result);
            //}
            //return JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
            return null;
        }

        public async Task<MainModulePlaceHolderModel> SaveMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder, CancellationToken token)
        {
            //var content = JsonConvert.SerializeObject(mainModulePlaceHolder);
            //var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            //var response = await _client.PostAsync(@"MainModulePlaceHolder?" + _userParameter, httpContent, token);
            //var result = await response.Content.ReadAsStringAsync();
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{
            //    throw new ApiResponseException(result);
            //}
            //return JsonConvert.DeserializeObject<MainModulePlaceHolderModel>(result);
            return null;
        }

        public async Task DeleteMainModulePlaceHolderAsync(int id, CancellationToken token)
        {
            //var response = await _client.DeleteAsync(@"MainModulePlaceHolder/" + id.ToString() + "?" + _userParameter, token);     
            //try
            //{
            //    response.EnsureSuccessStatusCode();
            //}
            //catch
            //{               
            //    throw new ApiResponseException(await response.Content.ReadAsStringAsync());
            //}       
        }
    }
}
