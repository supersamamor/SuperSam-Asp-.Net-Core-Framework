using CompanyNamePlaceHolder.Common.API.Swagger;
using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using CompanyNamePlaceHolder.Common.Web.Utility.Authorization;
using CompanyNamePlaceHolder.Common.Web.Utility.Identity;
using CompanyNamePlaceHolder.Common.Web.Utility.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Validation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CompanyNamePlaceHolder.Common.API;

public static class CommonApiExtensions
{
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

    public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options => options.ReportApiVersions = true);
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        return services;
    }

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

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services;
    }
}
