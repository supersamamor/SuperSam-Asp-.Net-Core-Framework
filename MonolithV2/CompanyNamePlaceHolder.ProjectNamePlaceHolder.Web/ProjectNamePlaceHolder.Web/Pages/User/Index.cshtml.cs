using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using X.PagedList;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Application.ApplicationServices.User;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    public class IndexModel : BasePageModel
    {
        private readonly UserService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public IndexModel(UserService service, IOptions<ProjectNamePlaceHolderWebConfig> appSetting, 
            ILogger<IndexModel> logger, ICorrelationContextAccessor correlationContext) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;           
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
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync));
            }               
        }

        private async Task GetUserListAsync() {
            var paginatedUser = await _service.GetUserListAsync(SearchKey,OrderBy, SortBy, PageNumber, PageSize);
            UserList = paginatedUser;
        }
    }
}
