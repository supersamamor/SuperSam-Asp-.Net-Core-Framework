using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var manager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            if (await manager.FindByClientIdAsync(defaultClientId) == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = defaultClientId,
                    ClientSecret = configuration.GetValue<string>("DefaultClient:ClientSecret"),
                    DisplayName = "Default client",
                    RedirectUris =
                        {
                            new Uri("https://oauth.pstmn.io/v1/callback")
                        },
                    Permissions =
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
                            Permissions.Prefixes.Scope + AuthorizationClaimTypes.Permission,
                            Permissions.Prefixes.Scope + "demo_api"
                        }
                });
            }
        }

        static async Task RegisterScopes(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictScopeManager>();
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
