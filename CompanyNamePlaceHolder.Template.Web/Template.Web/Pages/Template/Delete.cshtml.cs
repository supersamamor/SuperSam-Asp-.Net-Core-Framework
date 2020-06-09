using System;
using System.Threading;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Template.Web.ApiServices.Template;
using Template.Web.Extensions;
using Template.Web.Models.Template;

namespace Template.Web.Pages.Template
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly TemplateAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public DeleteModel(TemplateAPIService service, ILogger<DeleteModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        [BindProperty]
        public TemplateModel Template { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetTemplateItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync), Template);
            }          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await DeleteTemplateAsync(id);
                TempData["Success"] = Resource.PromptMessageDeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), Template);
                return Page();
            }          
            return RedirectToPage("./Index");
        }

        private async Task DeleteTemplateAsync(int id)
        {
            await _service.DeleteTemplateAsync(id, new CancellationToken());
        }

        private async Task GetTemplateItemAsync(int id)
        {
            Template = await _service.GetTemplateItemAsync(id, new CancellationToken());
        }
    }
}
