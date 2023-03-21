using CorrelationId;
using Cti.Core.Application.Common.Interfaces;
using Cti.Core.Logging.Serilog;
using FluentValidation.AspNetCore;
using CHANGE_TO_APP_NAME.Services.Application;
using CHANGE_TO_APP_NAME.Services.Infrastructure;
using CHANGE_TO_APP_NAME.Services.Infrastructure.Configurations;
using CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence;
using CHANGE_TO_APP_NAME.Services.API.Helpers;
using CHANGE_TO_APP_NAME.Services.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace CHANGE_TO_APP_NAME.Services.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            var _appOptions = Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();

            //services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCorrelationIdLogEnricher();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddDataProtection()
                .SetApplicationName(_appOptions.Name)
                .SetDefaultKeyLifetime(TimeSpan.FromDays(90))
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo("dataprotectionkeys"))
                .AddKeyManagementOptions(o => {
                    o.AutoGenerateKeys = true;
                    o.NewKeyLifetime = TimeSpan.FromDays(75);
                });

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllers(options => {
                options.Filters.Add<ApiExceptionFilterAttribute>();
                options.Filters.Add(new ProducesResponseTypeAttribute(200));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationProblemDetails), 400));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), 401));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), 404));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), 500));
            })
                    .AddFluentValidation();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            });

            services.AddApiVersioning(options =>
            {
                // Add the headers "api-supported-versions" and "api-deprecated-versions"
                // This is better for discoverability
                options.ReportApiVersions = true;

                // AssumeDefaultVersionWhenUnspecified should only be enabled when supporting legacy services that did not previously
                // support API versioning. Forcing existing clients to specify an explicit API version for an
                // existing service introduces a breaking change. Conceptually, clients in this situation are
                // bound to some API version of a service, but they don't know what it is and never explicit request it.
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                // Defines how an API version is read from the current HTTP request
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;

                options.DefaultApiVersionParameterDescription = "Do NOT modify api-version!";
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => {
                options.DocumentFilter<SwaggerDocFilter>();
                options.OperationFilter<SwaggerDefaultValues>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionProvider)
        {
            app.UseCorrelationId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(settings =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in apiVersionProvider.ApiVersionDescriptions.OrderByDescending(x => x.GroupName))
                {
                    settings.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                var _apiOptions = Configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();

                settings.EnableDeepLinking();
                settings.OAuthScopes(_apiOptions.OidcApiName);
                settings.OAuthUsePkce();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
