using CTI.Common.Web.Utility.Authorization;
using CTI.Common.Web.Utility.Extensions;
using CTI.ContractManagement.Web.Areas.Admin.Models;
using CTI.ContractManagement.Infrastructure.Data;
using CTI.ContractManagement.Web.Models;
using CTI.ContractManagement.Core.Oidc;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using System.Text.Json;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CTI.ContractManagement.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.Create)]
public class AddModel : BasePageModel<AddModel>
{
    private readonly OpenIddictApplicationManager<OidcApplication> _manager;
    private readonly IdentityContext _context;

    public AddModel(OpenIddictApplicationManager<OidcApplication> manager, IdentityContext context)
    {
        _manager = manager;
        _context = context;
    }

    [BindProperty]
    public ApplicationViewModel Application { get; set; } = new();

    [BindProperty]
    public IList<PermissionViewModel> ApplicationPermissions { get; set; } = new List<PermissionViewModel>();

    public async Task<IActionResult> OnGet()
    {
        ApplicationPermissions = Permission.GenerateAllPermissions().Map(p => new PermissionViewModel { Permission = p }).ToList();
        Application.Entities = await _context.GetEntitiesList(Application.EntityId);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Application.Entities = await _context.GetEntitiesList(Application.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return (await CreateApplication())
            .ToActionResult(
            success: application =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Created Application. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
                TempData.Put("Application", Application);
                return RedirectToPage("Details");
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                return Page();
            });
    }

    async Task<Validation<Error, ApplicationViewModel>> CreateApplication()
    {
        return await TryAsync(async () =>
        {
            var redirectUris = new System.Collections.Generic.HashSet<Uri> { new(Application.RedirectUri) };
            var permissions = new System.Collections.Generic.HashSet<string>
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Device,
                Permissions.Endpoints.Logout,
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.AuthorizationCode,
                Permissions.GrantTypes.ClientCredentials,
                Permissions.GrantTypes.DeviceCode,
                Permissions.GrantTypes.Password,
                Permissions.GrantTypes.RefreshToken,
                Permissions.ResponseTypes.Code,
                Permissions.Prefixes.Scope + AuthorizationClaimTypes.Permission,
            };
            Application.ClientId = Guid.NewGuid().ToString();
            Application.ClientSecret = Guid.NewGuid().ToString();
            permissions = permissions.Append(Application.Scopes.Split(" ")
                                                               .Map(e => Permissions.Prefixes.Scope + e))
                                     .ToHashSet();
            permissions = permissions.Append(ApplicationPermissions.Filter(e => e.Enabled)
                                                                   .Map(e => Permissions.Prefixes.Scope + e.Permission))
                                     .ToHashSet();
            var application = new OidcApplication
            {
                ClientId = Application.ClientId,
                DisplayName = Application.DisplayName,
                RedirectUris = JsonSerializer.Serialize(redirectUris),
                Permissions = JsonSerializer.Serialize(permissions),
                Entity = Application.EntityId,
            };
            await _manager.CreateAsync(application, Application.ClientSecret);
            return Success<Error, ApplicationViewModel>(Application);
        }).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception in OnPostAddAsync");
            return Fail<Error, ApplicationViewModel>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
        });
    }
}
