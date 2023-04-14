using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CelerSoft.Common.API.Swagger;

/// <summary>
/// A class for applying default configuration for generating Swagger documentation.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider _provider;
    readonly string _appName;

    /// <summary>
    /// Creates an instance of <see cref="ConfigureSwaggerOptions"/>.
    /// </summary>
    /// <param name="provider">Instance of <see cref="IApiVersionDescriptionProvider"/></param>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/></param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
        _provider = provider;
        _appName = configuration.GetValue<string>("Application");
    }

    /// <summary>
    /// Configures the Swagger documentation.
    /// </summary>
    /// <param name="options">Instance of <see cref="SwaggerGenOptions"/></param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo()
                {
                    Title = $"{_appName} {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                });
        }
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Input your access token here to access this API",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                }, new List<string>()
            }
        });
    }
}
