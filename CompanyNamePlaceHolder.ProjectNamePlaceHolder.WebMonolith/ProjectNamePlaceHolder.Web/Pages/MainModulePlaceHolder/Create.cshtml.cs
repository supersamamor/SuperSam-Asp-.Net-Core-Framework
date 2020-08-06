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
    public class CreateModel : PageModel
    {
        private readonly MainModulePlaceHolderService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public CreateModel(MainModulePlaceHolderService service, ILogger<CreateModel> logger, ICorrelationContextAccessor correlationContext)
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
            MainModulePlaceHolder = await _service.SaveMainModulePlaceHolderAsync(MainModulePlaceHolder);
        }
    }
}
