using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Web.ApiServices.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        private readonly MainModulePlaceHolderAPIService _service;
        private readonly ILogger _logger;     

        public IndexModel(MainModulePlaceHolderAPIService service, IOptions<ProjectNamePlaceHolderWebConfig> appSetting, 
            ILogger<IndexModel> logger) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;           
        }

        public IPagedList<MainModulePlaceHolderModel> MainModulePlaceHolderList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }

        public async Task OnGetAsync()
        {           
            try
            {
                await GetMainModulePlaceHolderListAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, nameof(OnGetAsync));
            }               
        }

        private async Task GetMainModulePlaceHolderListAsync() {
            var paginatedMainModulePlaceHolder = await _service.GetMainModulePlaceHolderListAsync(SearchKey,OrderBy, SortBy, PageNumber, PageSize, new CancellationToken());
            MainModulePlaceHolderList = paginatedMainModulePlaceHolder;
        }
    }
}
