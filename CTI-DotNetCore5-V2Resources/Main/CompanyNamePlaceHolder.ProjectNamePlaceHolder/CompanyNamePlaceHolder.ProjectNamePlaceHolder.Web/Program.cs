using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateBootstrapLogger();
            Log.Information("Starting up!");
            try
            {
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                await SeedDatabase(scope);
                Log.Information("Application starting");
                host.Run();
                Log.Information("Stopped cleanly");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task SeedDatabase(IServiceScope scope)
        {
            Log.Information("Seeding database");
            await DefaultRole.Seed(scope.ServiceProvider);
            await DefaultEntity.Seed(scope.ServiceProvider);
            await DefaultUser.Seed(scope.ServiceProvider);
            await DefaultClient.Seed(scope.ServiceProvider);
            Log.Information("Finished seeding database");
        }
    }
}
