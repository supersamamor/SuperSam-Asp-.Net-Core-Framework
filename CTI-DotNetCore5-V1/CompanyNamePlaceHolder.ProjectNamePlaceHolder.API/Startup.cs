using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Authorization;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var issuer = new Uri(Configuration.GetValue<string>("Authentication:Issuer"));
            var audience = Configuration.GetValue<string>("Authentication:Audience");

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

            services.AddControllers();

            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            if (Configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase("ApplicationContext"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext")));
            }

            services.AddHttpContextAccessor();
            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddApplicationServices();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationContext>();

            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
               
        public void Configure(IApplicationBuilder app,
                              IApiVersionDescriptionProvider provider,
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
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
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
