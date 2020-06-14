using AutoMapper;
using CTI.Common.Logging.Extensions.AspNetCore;
using CTI.Common.Logging.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Identity.Data;
using Identity.Data.Repositories;
using Identity.WebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }
            
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogCorrelation();
            string conn = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(conn,
                    assembly => assembly.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProjectNamePlaceHolder Identity API",
                    Contact = new OpenApiContact
                    {
                        Name = "devteam@filinvestcity.com",
                    }
                });
            });
            services.AddSingleton(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Data.Models.ProjectNamePlaceHolderUser, UserModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.ProjectNamePlaceHolderUser, Core.Models.ProjectNamePlaceHolderUser>().ReverseMap();
                    cfg.CreateMap<Core.Models.ProjectNamePlaceHolderUser, UserModel>().ReverseMap();
                    cfg.CreateMap<IdentityUser, Core.Models.IdentityUser>().ReverseMap();
                    cfg.CreateMap<IdentityRole, RoleModel>().ReverseMap();
                }
            ));
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks().AddDbContextCheck<IdentityContext>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<UserRepository>();
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectNamePlaceHolder Identity - API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthorization();
            app.UseLogCorrelation();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
