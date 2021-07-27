using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Applications
{
    [Authorize(Policy = Permission.Applications.Edit)]
    public class EditModel : BasePageModel<EditModel>
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _manager;

        public EditModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public ApplicationViewModel? Application { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Optional(await _manager.FindByClientIdAsync(id))
                .ToActionResult(async application =>
                {
                    var descriptor = new OpenIddictApplicationDescriptor();
                    await _manager.PopulateAsync(descriptor, application!);
                    Application = new()
                    {
                        ClientId = descriptor!.ClientId!,
                        DisplayName = descriptor.DisplayName,
                        RedirectUri = string.Join(" ", descriptor.RedirectUris),
                        Scopes = string.Join(" ",
                                             descriptor.Permissions.Where(p => p.StartsWith(Permissions.Prefixes.Scope))
                                                                   .Map(p => p[4..]))
                    };
                    return Page();
                }, none: null);
        }

        public async Task<IActionResult> OnPostGenerateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return await Optional(await _manager.FindByClientIdAsync(Application!.ClientId))
                .ToActionResult(async application => await GenerateNewSecret(application!), none: null);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return await Optional(await _manager.FindByClientIdAsync(Application!.ClientId))
                .ToActionResult(async application => await UpdateApplication(application!), none: null);
        }

        async Task<IActionResult> GenerateNewSecret(OpenIddictEntityFrameworkCoreApplication application)
        {
            return await TryAsync<IActionResult>(async () =>
            {
                Application!.ClientSecret = Guid.NewGuid().ToString();
                var descriptor = new OpenIddictApplicationDescriptor();
                await _manager.PopulateAsync(descriptor, application!);
                descriptor.ClientSecret = Application.ClientSecret;
                await _manager.UpdateAsync(application, descriptor, new());
                NotyfService.Success(Localizer["Generated new client secret"]);
                Logger.LogInformation("Updated Client Secret. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
                TempData.Put("Application", Application!);
                return RedirectToPage("Details");
            }).IfFail(ex =>
            {
                ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                Logger.LogError(ex, "Exception in OnPostEditAsync");
                return Page();
            });
        }

        async Task<IActionResult> UpdateApplication(OpenIddictEntityFrameworkCoreApplication application)
        {
            return await TryAsync<IActionResult>(async () =>
            {
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
                };

                Application!.Scopes!.Split(" ")
                                    .ToList()
                                    .ForEach(scope => permissions.Add(Permissions.Prefixes.Scope + scope));

                var descriptor = new OpenIddictApplicationDescriptor();
                await _manager.PopulateAsync(descriptor, application!);
                descriptor.DisplayName = Application.DisplayName;
                descriptor.RedirectUris.Clear();
                descriptor.RedirectUris.Add(new(Application.RedirectUri!));
                descriptor.Permissions.Clear();
                descriptor.Permissions.UnionWith(permissions);

                await _manager.UpdateAsync(application, descriptor, new());
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Updated Application. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
                return RedirectToPage("View", new { handler = "Details", id = Application.ClientId });
            }).IfFail(ex =>
            {
                ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                Logger.LogError(ex, "Exception in OnPostEditAsync");
                return Page();
            });
        }
    }
}
