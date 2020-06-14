using System;
using System.Threading;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectNamePlaceHolder.Web.ApiServices.User;
using ProjectNamePlaceHolder.Web.Models.User;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.SecurityData;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    [Authorize(Roles = Roles.ADMIN)]
    public class ActivateModel : PageModel
    {
        private readonly UserAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;   
        public ActivateModel(UserAPIService service, ILogger<ActivateModel> logger, 
            ICorrelationContextAccessor correlationContext)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;      
        }
        public IEnumerable<SelectListItem> NatureList { get; set; }
        public IEnumerable<SelectListItem> TaskList { get; set; }
        public IEnumerable<SelectListItem> ProjectList { get; set; }
        public IEnumerable<SelectListItem> SubProjectList { get; set; }
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
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostAsync), User);
            }          
            return Page();
        }
               
        public async Task<IActionResult> OnPostAsync()
        {
          
            try {
                this.ValidateModelState();
                await ActivateUserAsync();
                ModelState.Clear();
                TempData["Success"] =  Resource.PromptMessageSaveSuccess;
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

        private async Task ActivateUserAsync()
        {
            AppUser = await _service.ActivateUserAsync(AppUser.Id, new CancellationToken());
        }       
    }
}
