using CTI.Common.Web.Utility.Authorization;
using CTI.Common.Web.Utility.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Oidc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity;

public class IdentityStartup
{
    public IdentityStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        if (Configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseInMemoryDatabase("IdentityContext");
                options.UseOpenIddict<OidcApplication, OidcAuthorization, OidcScope, OidcToken, string>();
            });
        }
        else
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
                options.UseOpenIddict<OidcApplication, OidcAuthorization, OidcScope, OidcToken, string>();
            });
        }

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        services.AddAuthentication();
        #region Add These to enable external login
        //.AddMicrosoftAccount(options =>
        //{
        //    options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
        //    options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
        //})
        //.AddGoogle(options =>
        //{
        //    options.ClientId = Configuration["Authentication:Google:ClientId"];
        //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
        //});
        #endregion
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();
        services.AddTransient<IAuthenticatedUser, DefaultAuthenticatedUser>();

        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = Claims.Role;
        });

        services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<IdentityContext>()
                           .ReplaceDefaultEntities<OidcApplication, OidcAuthorization, OidcScope, OidcToken, string>();
                    options.UseQuartz();
                })
                .AddServer(options =>
                {
                    // Enable the authorization, device, logout, token, userinfo and verification endpoints.
                    options.SetAuthorizationEndpointUris("/connect/authorize")
                           .SetDeviceEndpointUris("/connect/device")
                           .SetLogoutEndpointUris("/connect/logout")
                           .SetTokenEndpointUris("/connect/token")
                           .SetUserinfoEndpointUris("/connect/userinfo")
                           .SetVerificationEndpointUris("/connect/verify");

                    // Note: this sample uses the code, device code, password and refresh token flows, but you
                    // can enable the other flows if you need to support implicit or client credentials.
                    options.AllowAuthorizationCodeFlow()
                           .AllowDeviceCodeFlow()
                           .AllowClientCredentialsFlow()
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow();

                    // Supported scopes
                    options.RegisterScopes(Scopes.Email,
                                           Scopes.Profile,
                                           Scopes.Phone,
                                           Scopes.Roles,
                                           Scopes.OfflineAccess,
                                           CustomClaimTypes.Entity,
                                           AuthorizationClaimTypes.Permission);

                    // Register the signing and encryption credentials.
                    //if (context.HostingEnvironment.EnvironmentName == "Development")
                    //{
                    //    options.AddDevelopmentEncryptionCertificate()
                    //           .AddDevelopmentSigningCertificate();
                    //}

                    options.AddEncryptionCertificate(Configuration.GetValue<string>("SslThumbprint"))
                           .AddSigningCertificate(Configuration.GetValue<string>("SslThumbprint"));

                    // Force client applications to use Proof Key for Code Exchange (PKCE).
                    options.RequireProofKeyForCodeExchange();

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                           .EnableStatusCodePagesIntegration()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableTokenEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableVerificationEndpointPassthrough();

                    // Note: if you don't want to specify a client_id when sending
                    // a token or revocation request, uncomment the following line:

                    options.AcceptAnonymousClients();

                    // Note: if you want to process authorization and token requests
                    // that specify non-registered scopes, uncomment the following line:

                    options.DisableScopeValidation();

                    // Note: if you don't want to use permissions, you can disable
                    // permission enforcement by uncommenting the following lines:
                    //
                    // options.IgnoreEndpointPermissions()
                    //        .IgnoreGrantTypePermissions()
                    //        .IgnoreResponseTypePermissions()
                    //        .IgnoreScopePermissions();

                    // Note: when issuing access tokens used by third-party APIs
                    // you don't own, you can disable access token encryption:

                    options.DisableAccessTokenEncryption();
                })
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
    }
}
