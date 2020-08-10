using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models;
using X.PagedList;
using ProjectNamePlaceHolder.Application.ApplicationServices.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Data;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{   
    public class IndexModel : BasePageModel
    {
        private readonly MainModulePlaceHolderService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public IndexModel(MainModulePlaceHolderService service, IOptions<ProjectNamePlaceHolderWebConfig> appSetting, 
            ILogger<IndexModel> logger, ICorrelationContextAccessor correlationContext) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public IPagedList<MainModulePlaceHolderModel> MainModulePlaceHolderList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }
        [BindProperty]
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }

        public async Task OnGetAsync()
        {           
            try
            {
                await GetMainModulePlaceHolderListAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync));
            }               
        }

        public IActionResult OnGetCreate()
        {
            return Partial("_Create", new MainModulePlaceHolderModel());
        }   

        public async Task<IActionResult> OnPostSave()
        {
            try
            {
                this.ValidateModelState();
                await SaveMainModulePlaceHolderAsync();
                TempData["Success"] = Resource.PromptMessageSaveSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostSave), MainModulePlaceHolder);
                return Partial("_Create", MainModulePlaceHolder);
            }
            return Partial("_Edit", MainModulePlaceHolder);
        }

        public async Task<IActionResult> OnGetRecordAsync(int id, string pageName)
        {
            try
            {
                await GetMainModulePlaceHolderItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetRecordAsync), MainModulePlaceHolder);
            }
            return Partial(pageName, MainModulePlaceHolder);
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                this.ValidateModelState();
                await UpdateMainModulePlaceHolderAsync();
                TempData["Success"] = Resource.PromptMessageUpdateSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostUpdateAsync), MainModulePlaceHolder);          
            }
            return Partial("_Edit", MainModulePlaceHolder);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await DeleteMainModulePlaceHolderAsync(id);
                TempData["Success"] = Resource.PromptMessageDeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostDeleteAsync), MainModulePlaceHolder);
                return Page();
            }
            return Partial("_Delete", MainModulePlaceHolder);
        }

        private async Task GetMainModulePlaceHolderListAsync()
        {
            var mainModulePlaceHolderList = await _service.GetMainModulePlaceHolderListAsync(SearchKey, OrderBy, SortBy, PageNumber, PageSize);
            MainModulePlaceHolderList = new StaticPagedList<MainModulePlaceHolderModel>(mainModulePlaceHolderList.Items, 
                mainModulePlaceHolderList.PagedListMetaData.PageNumber, mainModulePlaceHolderList.PagedListMetaData.PageSize, 
                mainModulePlaceHolderList.PagedListMetaData.TotalItemCount);
        }

        private async Task SaveMainModulePlaceHolderAsync()
        {
            MainModulePlaceHolder = await _service.SaveMainModulePlaceHolderAsync(MainModulePlaceHolder);
        }

        private async Task GetMainModulePlaceHolderItemAsync(int id)
        {
            MainModulePlaceHolder = await _service.GetMainModulePlaceHolderItemAsync(id);
        }    

        private async Task UpdateMainModulePlaceHolderAsync()
        {
            MainModulePlaceHolder = await _service.UpdateMainModulePlaceHolderAsync(MainModulePlaceHolder);
        }

        private async Task DeleteMainModulePlaceHolderAsync(int id)
        {
            await _service.DeleteMainModulePlaceHolderAsync(id);
        }
    }
}
