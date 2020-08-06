using System;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Application.ApplicationServices.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Web.Extensions;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly MainModulePlaceHolderService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;
        public EditModel(MainModulePlaceHolderService service, ILogger<EditModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        [BindProperty]
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetMainModulePlaceHolderItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync), MainModulePlaceHolder);
            }          
            return Page();
        }
               
        public async Task<IActionResult> OnPostAsync()
        {        
            try {
                this.ValidateModelState();
                await UpdateMainModulePlaceHolderAsync();
                TempData["Success"] = Resource.PromptMessageUpdateSuccess;
            }          
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), MainModulePlaceHolder);
            }         
            return Page();
        }

        private async Task GetMainModulePlaceHolderItemAsync(int id)
        {
            MainModulePlaceHolder = await _service.GetMainModulePlaceHolderItemAsync(id);
        }

        private async Task UpdateMainModulePlaceHolderAsync()
        {
            MainModulePlaceHolder = await _service.UpdateMainModulePlaceHolderAsync(MainModulePlaceHolder);
        }
    }
}
