using CompanyPL.EISPL.Web.Areas.Admin.Commands.AuditTrail;
using CompanyPL.EISPL.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CompanyPL.EISPL.Core.Identity;

namespace CompanyPL.EISPL.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LogoutModel : BasePageModel<LogoutModel>
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
        await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged out", TraceId = TraceId });
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
        await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged out", TraceId = TraceId });
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
