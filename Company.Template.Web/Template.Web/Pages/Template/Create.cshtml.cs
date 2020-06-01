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
    public class CreateModel : PageModel
    {
        private readonly TemplateAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public CreateModel(TemplateAPIService service, ILogger<EditModel> logger, ICorrelationContextAccessor correlationContext)
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
        public TemplateModel Template { get; set; }
      
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                this.ValidateModelState();
                await SaveTemplateAsync();
                TempData["Success"] = Resource.PromptMessageSaveSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), Template);
                return Page();
            }
            return RedirectToPage("./Edit", new { id = Template.Id });
        }

        private async Task SaveTemplateAsync()
        {
            Template = await _service.SaveTemplateAsync(Template, new CancellationToken());
        }
    }
}
