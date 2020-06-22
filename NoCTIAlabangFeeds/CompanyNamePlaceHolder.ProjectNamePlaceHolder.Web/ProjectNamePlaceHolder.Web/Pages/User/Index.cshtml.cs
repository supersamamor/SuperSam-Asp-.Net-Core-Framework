using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using ProjectNamePlaceHolder.Web.ApiServices.User;
using ProjectNamePlaceHolder.SecurityData;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.User;
using ProjectNamePlaceHolder.Web.Extensions;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    [Authorize(Roles = Roles.ADMIN)]
    public class IndexModel : BasePageModel
    {
        private readonly UserAPIService _service;
        private readonly ILogger _logger;


        public IndexModel(UserAPIService service, IOptions<ProjectNamePlaceHolderWebConfig> appSetting, 
            ILogger<IndexModel> logger) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;              
        }

        public IPagedList<UserModel> UserList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }      
        public async Task OnGetAsync()
        {           
            try
            {
                await GetUserListAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, nameof(OnGetAsync));
            }               
        }

        private async Task GetUserListAsync() {
            var paginatedUser = await _service.GetUserListAsync(SearchKey,OrderBy, SortBy, PageNumber, PageSize, new CancellationToken());
            UserList = paginatedUser;
        }
    }
}
