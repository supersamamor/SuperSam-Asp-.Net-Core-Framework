using System;
using System.Threading;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.SecurityData;
using ProjectNamePlaceHolder.Web.ApiServices.User;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models.User;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    [Authorize(Roles = Roles.ADMIN)]
    public class EditModel : PageModel
    {
        private readonly UserAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public EditModel(UserAPIService service, ILogger<EditModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }
        [BindProperty]
        public UserModel AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetUserItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync), id);
            }           
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                this.ValidateModelState();
                await UpdateUserAsync();
                ModelState.Clear();
                TempData["Success"] = Resource.PromptMessageSaveSuccess;
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), User);
            }
            return Page();
        }
        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id, new CancellationToken());
        }
        private async Task UpdateUserAsync()
        {
            AppUser = await _service.UpdateUserAsync(AppUser, new CancellationToken());
        }
    }
}
