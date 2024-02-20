using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProjectNamePlaceHolder.Web.Services.Email;
using ProjectNamePlaceHolder.Data;
using MediatR;
using ProjectNamePlaceHolder.Data.Repositories;
using AutoMapper;
using ProjectNamePlaceHolder.Logger.Extensions.DependencyInjection;
using ProjectNamePlaceHolder.Logger.Extensions.AspNetCore;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Application.Models.Role;
using ProjectNamePlaceHolder.Application.ApplicationServices.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.ApplicationServices.Role;
using ProjectNamePlaceHolder.Application.ApplicationServices.User;
using ProjectNamePlaceHolder.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using ProjectNamePlaceHolder.Logger.Filters;

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
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSession();
            services.AddDbContext<ProjectNamePlaceHolderContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProjectNamePlaceHolderContext>();

            services.AddSingleton(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.MainModulePlaceHolder, Core.Models.MainModulePlaceHolder>().ReverseMap();
                    cfg.CreateMap<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.ProjectNamePlaceHolderUser, UserModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.ProjectNamePlaceHolderUser, Core.Models.ProjectNamePlaceHolderUser>().ReverseMap();
                    cfg.CreateMap<Core.Models.ProjectNamePlaceHolderUser, UserModel>().ReverseMap();
                    cfg.CreateMap<IdentityUser, Core.Models.IdentityUser>().ReverseMap();
                    cfg.CreateMap<IdentityRole, RoleModel>().ReverseMap();
                }
            ));

            #region Application Services
            services.AddTransient<MainModulePlaceHolderService>();
            services.AddTransient<UserService>();
            services.AddTransient<RoleService>();
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

            services.AddTransient<IEmailSender, SMTPEmailService>();
            services.AddRazorPages();
            services.AddHealthChecks().AddDbContextCheck<ProjectNamePlaceHolderContext>();
            services.AddApplicationInsightsTelemetry();
            services.AddMediatR(typeof(Resource).Assembly);
            services.AddTransient<MainModulePlaceHolderRepository>();
            services.AddTransient<UserRepository>();
            services.AddHealthChecks().AddSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #region Api
            services.AddHttpContextAccessor();

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add<SerilogLoggingActionFilter>();
            });
        
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProjectNamePlaceHolder API",
                    Description = "Web APIs for accessing ProjectNamePlaceHolder resources",
                });
            });
            #endregion
            services.AddRazorPages().AddRazorRuntimeCompilation();
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
            #region Api
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectNamePlaceHolder API");
            });
            app.UseLogCorrelation();
            #endregion
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseLogCorrelation();   
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
