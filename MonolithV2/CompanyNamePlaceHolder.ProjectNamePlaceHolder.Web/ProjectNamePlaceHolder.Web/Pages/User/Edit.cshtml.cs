﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Application.ApplicationServices.Role;
using ProjectNamePlaceHolder.Application.ApplicationServices.User;
using ProjectNamePlaceHolder.Application.Models.Role;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Web.Extensions;

namespace ProjectNamePlaceHolder.Web.Pages.User
{
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
            var roleToAdd = AppUser.RoleSelection.Where(l => l.Id == addRoleId).FirstOrDefault();
            AppUser.UserRoles.Add(roleToAdd);
            AppUser.RoleSelection.Remove(roleToAdd);
            return Page();
        }

        public IActionResult OnPostRemoveRole(string removeRoleId)
        {
            var roleToRemove = AppUser.UserRoles.Where(l => l.Id == removeRoleId).FirstOrDefault();
            AppUser.RoleSelection.Add(roleToRemove);
            AppUser.UserRoles.Remove(roleToRemove);
            return Page();
        }

        private async Task GetUserItemAsync(int id)
        {
            AppUser = await _service.GetUserItemAsync(id);
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
    }
}
