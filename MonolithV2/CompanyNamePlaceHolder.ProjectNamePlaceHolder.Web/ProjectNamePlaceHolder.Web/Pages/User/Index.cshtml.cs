using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using X.PagedList;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Application.ApplicationServices.User;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Application.Models.Role;
using System.Collections.Generic;
using ProjectNamePlaceHolder.Application.ApplicationServices.Role;
using ProjectNamePlaceHolder.Application;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    public class IndexModel : BasePageModel
    {
        private readonly UserService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;
        private readonly RoleService _roleService;
        public IndexModel(UserService service, IOptions<ProjectNamePlaceHolderWebConfig> appSetting, 
            ILogger<IndexModel> logger, ICorrelationContextAccessor correlationContext, RoleService roleService) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
            _roleService = roleService;
        }

        public IPagedList<UserModel> UserList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }   
        [BindProperty]
        public UserModel AppUser { get; set; }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetInitializeListAsync()
        {
            try
            {
                await GetUserListAsync();
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetInitializeListAsync));
            }
            return Partial("_List", this);
        }

        public async Task<IActionResult> OnGetShowEdit(int id)
        {
            await GetRecordAsync(id);
            await GetRoleDropdowns();
            return Partial("_Edit", AppUser);
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                this.ValidateModelState();
                await UpdateUserAsync();
                TempData[PromptContainerMessageTempDataName.Success] = Resource.PromptMessageUpdateSuccess;
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnPostUpdateAsync), AppUser);
            }
            return Partial("_Edit", AppUser);
        }
        public async Task<IActionResult> OnGetShowView(int id)
        {
            await GetRecordAsync(id);
            return Partial("_View", AppUser);
        }

        private async Task GetUserListAsync() 
        {
            var paginatedUser = await _service.GetUserListAsync(SearchKey, OrderBy, SortBy, PageNumber, PageSize);
            UserList = paginatedUser;
        }

        private async Task UpdateUserAsync()
        {
            AppUser = await _service.UpdateUserAsync(AppUser);
        }

        private async Task GetRoleDropdowns()
        {
            AppUser.UserRoles = await _roleService.GetCurrentRoleListAsync(AppUser.Id);
            AppUser.RoleSelection = await _roleService.GetAvailableRoleListAsync(AppUser.Id);
        }
        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id);
        }

        private async Task GetRecordAsync(int id)
        {
            try
            {
                await GetUserItemAsync(id);
            }
            catch (Exception ex)
            {
                TempData[PromptContainerMessageTempDataName.Error] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(GetRecordAsync), User);
            }
        }
    }
}
