using System;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Application.ApplicationServices.User;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Web.Extensions;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DetailsModel : PageModel
    {
        private readonly UserService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public DetailsModel(UserService service, ILogger<DetailsModel> logger, ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

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

        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id);
        }
    }
}
