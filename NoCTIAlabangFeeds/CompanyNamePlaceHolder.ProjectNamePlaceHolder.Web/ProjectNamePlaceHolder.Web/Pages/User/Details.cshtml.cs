using System;
using System.Threading;
using System.Threading.Tasks;
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
    public class DetailsModel : PageModel
    {
        private readonly UserAPIService _service;
        private readonly ILogger _logger;  

        public DetailsModel(UserAPIService service, ILogger<DetailsModel> logger)
        {
            _service = service;
            _logger = logger;    
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
                TempData["Error"] = _logger.CustomErrorLogger(ex, nameof(OnGetAsync), id);
            }           
            return Page();
        }

        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id, new CancellationToken());
        }
    }
}
