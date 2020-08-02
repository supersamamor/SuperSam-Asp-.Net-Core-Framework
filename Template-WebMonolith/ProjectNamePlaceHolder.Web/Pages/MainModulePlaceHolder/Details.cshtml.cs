using System;
using System.Threading;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Web.ApplicationServices.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MainModulePlaceHolderService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public DetailsModel(MainModulePlaceHolderService service, ILogger<DetailsModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetMainModulePlaceHolderItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync), id);
            }           
            return Page();
        }

        private async Task GetMainModulePlaceHolderItemAsync(int id)
        {
            MainModulePlaceHolder = await _service.GetMainModulePlaceHolderItemAsync(id);
        }
    }
}
