using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
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
    [Authorize(Policy = Permission.Applications.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _manager;

        public AddModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public ApplicationViewModel? Application { get; set; }

        public IActionResult OnGetAdd()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return (await CreateApplication()).Match<IActionResult>(
                Succ: application =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Created Application. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
                    TempData.Put("Application", Application!);
                    return RedirectToPage("Details");
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<Validation<Error, ApplicationViewModel>> CreateApplication()
        {
            return await TryAsync(async () =>
            {
                Application!.ClientId = Guid.NewGuid().ToString();
                Application.ClientSecret = Guid.NewGuid().ToString();

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

                Application.Scopes!.Split(" ")
                                   .ToList()
                                   .ForEach(scope => permissions.Add(Permissions.Prefixes.Scope + scope));

                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = Application.ClientId,
                    ClientSecret = Application.ClientSecret,
                    DisplayName = Application.DisplayName
                };
                descriptor.RedirectUris.Add(new(Application.RedirectUri!));
                descriptor.Permissions.UnionWith(permissions);

                await _manager.CreateAsync(descriptor);
                return Success<Error, ApplicationViewModel>(Application);
            }).IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPostAddAsync");
                return Fail<Error, ApplicationViewModel>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            });
        }
    }
}
