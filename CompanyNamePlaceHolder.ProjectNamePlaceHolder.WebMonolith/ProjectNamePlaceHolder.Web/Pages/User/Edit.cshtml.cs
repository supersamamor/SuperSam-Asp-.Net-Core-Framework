﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Web.ApplicationServices.Role;
using ProjectNamePlaceHolder.Web.ApplicationServices.User;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models.Role;
using ProjectNamePlaceHolder.Web.Models.User;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
    [Authorize(Roles = Roles.ADMIN)]
    public class EditModel : PageModel
    {
        private readonly UserService _service;
        private readonly RoleService _roleService;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public EditModel(UserService service, ILogger<EditModel> logger, ICorrelationContextAccessor correlationContext, RoleService roleService)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
            _roleService = roleService;
        }
        [BindProperty]
        public UserModel AppUser { get; set; }
        [BindProperty]
        public IList<RoleModel> CurrentUserRoles { get; set; }
        [BindProperty]
        public IList<RoleModel> RoleSelection { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetUserItemAsync(id);
                await GetRoleDropdowns();
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

        public IActionResult OnPostAddRole(string addRoleId)
        {
            var roleToAdd = RoleSelection.Where(l => l.Id == addRoleId).FirstOrDefault();
            CurrentUserRoles.Add(roleToAdd);
            RoleSelection.Remove(roleToAdd);
            return Page();
        }

        public IActionResult OnPostRemoveRole(string removeRoleId)
        {
            var roleToRemove = CurrentUserRoles.Where(l => l.Id == removeRoleId).FirstOrDefault();
            RoleSelection.Add(roleToRemove);
            CurrentUserRoles.Remove(roleToRemove);
            return Page();
        }

        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id);
        }

        private async Task UpdateUserAsync()
        {
            AppUser.UserRoles = CurrentUserRoles;
            AppUser = await _service.UpdateUserAsync(AppUser);
        }

        private async Task GetRoleDropdowns()
        {
            CurrentUserRoles = await _roleService.GetCurrentRoleListAsync(AppUser.Id);
            RoleSelection = await _roleService.GetAvailableRoleListAsync(AppUser.Id);
        }
    }
}
