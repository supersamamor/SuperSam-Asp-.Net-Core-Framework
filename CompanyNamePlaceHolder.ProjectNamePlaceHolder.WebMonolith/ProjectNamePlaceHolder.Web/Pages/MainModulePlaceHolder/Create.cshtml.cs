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
    public class CreateModel : PageModel
    {
        private readonly MainModulePlaceHolderAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public CreateModel(MainModulePlaceHolderAPIService service, ILogger<CreateModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
      
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                this.ValidateModelState();
                await SaveMainModulePlaceHolderAsync();
                TempData["Success"] = Resource.PromptMessageSaveSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), MainModulePlaceHolder);
                return Page();
            }
            return RedirectToPage("./Edit", new { id = MainModulePlaceHolder.Id });
        }

        private async Task SaveMainModulePlaceHolderAsync()
        {
            MainModulePlaceHolder = await _service.SaveMainModulePlaceHolderAsync(MainModulePlaceHolder, new CancellationToken());
        }
    }
}
