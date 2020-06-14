using Correlate.DependencyInjection;
using CTI.Common.Logging.Extensions.AspNetCore;
using CTI.Common.Logging.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ProjectNamePlaceHolder.SecurityData;
using ProjectNamePlaceHolder.Web.ApiServices.MainModulePlaceHolder;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Web.ApiServices.User;

namespace ProjectNamePlaceHolder.Web
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
            services.AddLogCorrelation();
            services.AddOptions();
            services.Configure<ProjectNamePlaceHolderWebConfig>(Configuration.GetSection("ProjectNamePlaceHolderWebConfig"));
            services.AddSession();
            services.AddDbContext<ProjectNamePlaceHolderContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProjectNamePlaceHolderContext>();
         
            #region Api Services
            services.AddTransient<MainModulePlaceHolderAPIService>();
            services.AddHttpClient<MainModulePlaceHolderAPIService>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ProjectNamePlaceHolderWebConfig:ApiURLMainModulePlaceHolder"));              
            }).CorrelateRequests();

            services.AddTransient<UserAPIService>();
            services.AddHttpClient<UserAPIService>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ProjectNamePlaceHolderWebConfig:ApiURLIdentity"));
            }).CorrelateRequests();

            #endregion
            #region External Logins
            services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["AuthenticationConfig:MicrosoftClientId"];
                microsoftOptions.ClientSecret = Configuration["AuthenticationConfig:MicrosoftClientSecret"];
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["AuthenticationConfig:GoogleClientId"];
                googleOptions.ClientSecret = Configuration["AuthenticationConfig:GoogleClientSecret"];
            });
            #endregion         
            services.AddRazorPages();
            services.AddHealthChecks().AddDbContextCheck<ProjectNamePlaceHolderContext>();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseLogCorrelation();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
