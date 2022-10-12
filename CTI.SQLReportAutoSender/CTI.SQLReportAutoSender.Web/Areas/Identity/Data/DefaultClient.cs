using CTI.Common.Web.Utility.Authorization;
using CTI.Common.Web.Utility.Identity;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using CTI.SQLReportAutoSender.Core.Oidc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using System.Text.Json;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CTI.SQLReportAutoSender.Web.Areas.Identity.Data;

public static class DefaultClient
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<IdentityContext>();
        await context.Database.EnsureCreatedAsync(new CancellationToken());

        await RegisterApplications(serviceProvider);
        await RegisterScopes(serviceProvider);
    }

    static async Task RegisterApplications(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var defaultClientId = configuration.GetValue<string>("DefaultClient:ClientId");

        var context = serviceProvider.GetRequiredService<IdentityContext>();
        var entity = await context.Entities.FirstAsync(e => e.Name == "Default");

        var manager = serviceProvider.GetRequiredService<OpenIddictApplicationManager<OidcApplication>>();
        var redirectUris = new HashSet<Uri> { new("https://oauth.pstmn.io/v1/callback") };
        var permissions = new HashSet<string>
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
                Permissions.Scopes.Email,
                Permissions.Scopes.Profile,
                Permissions.Scopes.Roles,
                Permissions.Prefixes.Scope + Claims.Name,
                Permissions.Prefixes.Scope + CustomClaimTypes.Entity,
                Permissions.Prefixes.Scope + AuthorizationClaimTypes.Permission,
                Permissions.Prefixes.Scope + "demo_api",
            };
        permissions = permissions.Append(Permission.GenerateAllPermissions()
                                                   .Map(permission => Permissions.Prefixes.Scope + permission))
                                 .ToHashSet();
        await Optional(await manager.FindByClientIdAsync(defaultClientId)).MatchAsync(
            async application =>
            {
                application!.DisplayName = "Default client";
                application.RedirectUris = JsonSerializer.Serialize(redirectUris);
                application.Permissions = JsonSerializer.Serialize(permissions);
                application.Entity = entity.Id;
                await manager.UpdateAsync(application);
                return 0;
            },
            async () =>
            {
                await manager.CreateAsync(new OidcApplication
                {
                    ClientId = defaultClientId,
                    DisplayName = "Default client",
                    RedirectUris = JsonSerializer.Serialize(redirectUris),
                    Permissions = JsonSerializer.Serialize(permissions),
                    Entity = entity.Id,
                }, configuration.GetValue<string>("DefaultClient:ClientSecret"));
                return 0;
            });
    }

    static async Task RegisterScopes(IServiceProvider provider)
    {
        var manager = provider.GetRequiredService<OpenIddictScopeManager<OidcScope>>();
        if (await manager.FindByNameAsync("demo_api") is null)
        {
            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "Demo API access",
                Name = "demo_api",
                Resources =
                    {
                        "https://localhost:44379"
                    }
            });
        }
    }
}
