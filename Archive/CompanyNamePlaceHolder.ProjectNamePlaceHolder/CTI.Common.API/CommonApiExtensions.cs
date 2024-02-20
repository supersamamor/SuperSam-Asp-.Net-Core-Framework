using CTI.Common.API.Swagger;
using CTI.Common.Identity.Abstractions;
using CTI.Common.Web.Utility.Authorization;
using CTI.Common.Web.Utility.Identity;
using CTI.Common.Web.Utility.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Validation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CTI.Common.API;

/// <summary>
/// A class containing extensions for enabling common API services.
/// </summary>
public static class CommonApiExtensions
{
    /// <summary>
    /// Enables Swagger API documentation with common options.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication EnableSwagger(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        app.UseSwagger();
        var provider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
        return app;
    }

    /// <summary>
    /// Registers default API services. Optionally, you can call the other methods in this class
    /// to register the services individually.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDefaultApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.ConfigureAuthentication(configuration);
        services.ConfigureAuthorization();
        services.ConfigureVersioning();
        services.ConfigureSwagger();
        services.AddLogEnricherServices();
        services.AddApplicationInsightsTelemetry();
        services.AddTransient<IAuthenticatedUser, DefaultAuthenticatedUser>();
        return services;
    }

    /// <summary>
    /// Registers services related to API versioning with default options.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options => options.ReportApiVersions = true);
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        return services;
    }

    /// <summary>
    /// Registers services related to Swagger documentation with default options defined in <see cref="ConfigureSwaggerOptions"/>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        return services;
    }

    /// <summary>
    /// Registers services related to authentication.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = new Uri(configuration.GetValue<string>("Authentication:Issuer"));
        var audience = configuration.GetValue<string>("Authentication:Audience");
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(issuer);
                options.AddAudiences(audience);
                options.UseSystemNetHttp();
                options.UseAspNetCore();
            });
        return services;
    }

    /// <summary>
    /// Registers services related to authorization. Uses <see cref="PermissionPolicyProvider"/>
    /// and <see cref="PermissionAuthorizationHandler"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services;
    }
}
