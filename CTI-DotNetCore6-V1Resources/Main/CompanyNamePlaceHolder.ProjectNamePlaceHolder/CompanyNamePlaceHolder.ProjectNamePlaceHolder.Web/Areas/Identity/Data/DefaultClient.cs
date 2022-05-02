using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Authorization;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Oidc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data
{
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
            var entity = await context.Entities.FirstOrDefaultAsync(e => e.Name == "Default");

            var manager = serviceProvider.GetRequiredService<OpenIddictApplicationManager<OidcApplication>>();
            if (await manager.FindByClientIdAsync(defaultClientId) == null)
            {
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
                    Permissions.Prefixes.Scope + "demo_api",
                };
                permissions = permissions.Append(Permission.GenerateAllPermissions()
                                                           .Map(permission => Permissions.Prefixes.Scope + permission))
                                         .ToHashSet();
                await manager.CreateAsync(new OidcApplication
                {
                    ClientId = defaultClientId,
                    DisplayName = "Default client",
                    RedirectUris = JsonSerializer.Serialize(redirectUris),
                    Permissions = JsonSerializer.Serialize(permissions),
                    Entity = entity.Id,
                }, configuration.GetValue<string>("DefaultClient:ClientSecret"));
            }
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
}
