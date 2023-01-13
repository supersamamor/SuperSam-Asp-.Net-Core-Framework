using CTI.Common.Web.Utility.Authorization;
using CTI.Common.Web.Utility.Extensions;
using CTI.LocationApi.Web.Areas.Admin.Models;
using CTI.LocationApi.Infrastructure.Data;
using CTI.LocationApi.Web.Models;
using CTI.LocationApi.Core.Oidc;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CTI.LocationApi.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.View)]
public class ViewModel : BasePageModel<ViewModel>
{
    private readonly OpenIddictApplicationManager<OidcApplication> _manager;
    private readonly IdentityContext _context;

    public ViewModel(OpenIddictApplicationManager<OidcApplication> manager, IdentityContext context)
    {
        _manager = manager;
        _context = context;
    }

    public ApplicationViewModel Application { get; set; } = new();
    public IList<PermissionViewModel> ApplicationPermissions { get; set; } = new List<PermissionViewModel>();

    public async Task<IActionResult> OnGet(string? id)
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
                var scopes = descriptor.Permissions.Where(p => p.StartsWith(Permissions.Prefixes.Scope))
                                                   .Map(p => p[4..]);
                ApplicationPermissions = Permission.GenerateAllPermissions()
                                                   .Map(p => new PermissionViewModel
                                                   {
                                                       Permission = p,
                                                       Enabled = scopes.Any(s => s == p),
                                                   }).ToList();
                Application = new()
                {
                    ClientId = descriptor.ClientId ?? "",
                    DisplayName = descriptor.DisplayName ?? "",
                    RedirectUri = string.Join(" ", descriptor.RedirectUris),
                    Scopes = string.Join(" ", scopes.Where(s => !s.StartsWith(AuthorizationClaimTypes.Permission))),
                    Entity = await _context.GetEntityName(application!.Entity).IfNoneAsync(""),
                };
                return Page();
            }, none: null);
    }
}
