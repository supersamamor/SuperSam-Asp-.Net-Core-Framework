using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IMediator _mediator;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                           ILogger<LogoutModel> logger, IMediator mediator)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged out" });
            _logger.LogInformation("User logged out, Email = {Email}", user.Email);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged out" });
            _logger.LogInformation("User logged out, Email = {Email}", user.Email);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
