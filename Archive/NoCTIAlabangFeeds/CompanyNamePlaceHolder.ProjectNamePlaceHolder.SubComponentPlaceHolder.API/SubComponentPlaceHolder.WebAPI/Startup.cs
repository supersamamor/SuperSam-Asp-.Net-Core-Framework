using AutoMapper;
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
using SubComponentPlaceHolder.Data;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;
using Serilog;

namespace SubComponentPlaceHolder.WebAPI
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
            string conn = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SubComponentPlaceHolderContext>(options =>
            {
                options.UseSqlServer(conn,
                    assembly => assembly.MigrationsAssembly(typeof(SubComponentPlaceHolderContext).Assembly.FullName));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProjectNamePlaceHolder SubComponentPlaceHolder API",
                    Contact = new OpenApiContact
                    {
                        Name = "devteam@filinvestcity.com",
                    }
                });
            });
            services.AddSingleton(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.MainModulePlaceHolder, Core.Models.MainModulePlaceHolder>().ReverseMap();
                    cfg.CreateMap<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>().ReverseMap();
                }
            ));
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks().AddDbContextCheck<SubComponentPlaceHolderContext>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<MainModulePlaceHolderRepository>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectNamePlaceHolder SubComponentPlaceHolder - API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
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
