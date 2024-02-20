using CTI.DSF.Web.Areas.Admin.Commands.AuditTrail;
using CTI.DSF.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.DSF.Core.Identity;

namespace CTI.DSF.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LoginModel : BasePageModel<LoginModel>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public LoginModel(SignInManager<ApplicationUser> signInManager,
                      UserManager<ApplicationUser> userManager,
                      IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
    }

    [BindProperty]
    public InputModel? Input { get; set; }

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }

    public string? ReturnUrl { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(Input!.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                Logger.LogWarning("Invalid login attempt, Email = {Email}", Input!.Email);
                return Page();
            }
            if (!user.IsActive)
            {
                await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User is not active", TraceId = TraceId });
                Logger.LogWarning("User is not active, Email = {Email}", Input!.Email);
                return RedirectToPage("./NotActive");
            }
            var result = await _signInManager.PasswordSignInAsync(Input!.Email, Input!.Password, Input!.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged in", TraceId = TraceId });
                Logger.LogInformation("User logged in, Email = {Email}", Input!.Email);
                NotyfService.Success($"Logged in as {Input!.Email}");
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User account locked out", TraceId = TraceId });
                Logger.LogWarning("User account locked out, Email = {Email}", Input!.Email);
                return RedirectToPage("./Lockout");
            }
            else
            {
                await _mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "Invalid login attempt", TraceId = TraceId });
                user.LastUnsucccessfulLogin = DateTime.UtcNow;
                _ = await _userManager.UpdateAsync(user);
                Logger.LogWarning("Invalid login attempt, Email = {Email}", Input!.Email);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
