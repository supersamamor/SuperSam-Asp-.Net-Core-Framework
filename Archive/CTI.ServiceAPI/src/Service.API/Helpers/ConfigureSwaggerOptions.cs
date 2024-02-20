using Cti.Core.Application.Common.Interfaces;
using ProjectNamePlaceHolder.Services.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.API.Helpers
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;
        private readonly IApp _app;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration, IApp app) 
        {
            _provider = provider;
            _configuration = configuration;
            _app = app;
        }

        public void Configure(SwaggerGenOptions options)
        {
            var _apiOptions = _configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differentlys
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            //Add Comments
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            var _appXmlDoc = $"{Assembly.GetAssembly(typeof(Application.DependencyInjection)).GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, _appXmlDoc));

            var _schemaHelper = new SwaggerSchemaHelper();
            options.CustomOperationIds(o => o.ActionDescriptor.RouteValues["action"]);
            options.CustomSchemaIds(type => _schemaHelper.GetSchemaId(type));

            //Add Swagger Security
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                In = ParameterLocation.Header,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{_apiOptions.IdentityServerBaseUrl.TrimEnd('/')}/connect/authorize"),
                        TokenUrl = new Uri($"{_apiOptions.IdentityServerBaseUrl.TrimEnd('/')}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { _apiOptions.OidcApiName, "general access" }
                        }
                    },
                    ClientCredentials = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{_apiOptions.IdentityServerBaseUrl.TrimEnd('/')}/connect/authorize"),
                        TokenUrl = new Uri($"{_apiOptions.IdentityServerBaseUrl.TrimEnd('/')}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { _apiOptions.OidcApiName, "general access" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="OAuth2"
                        }
                    },
                    new string[]{}
                }
            });
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _app.Name,
                Version = description.ApiVersion.ToString(),
                Description = "Web APIs for " + _app.Name,
                TermsOfService = null,
                Contact = new OpenApiContact
                {
                    Name = "CTI",
                    Email = "appsdev@corptech.it",
                    Url = new Uri("https://www.filinvest.com.ph")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("https://www.filinvest.com.ph")
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
