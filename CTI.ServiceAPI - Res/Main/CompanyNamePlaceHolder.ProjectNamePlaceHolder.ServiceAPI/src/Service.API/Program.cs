using Cti.Core.Logging.Serilog;
using ProjectNamePlaceHolder.Services.Infrastructure.Configurations;
using ProjectNamePlaceHolder.Services.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var configuration = GetConfiguration(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateBootstrapLogger();

            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())                 
                {
                    var services = scope.ServiceProvider;

                    var _config = configuration.GetSection(nameof(DbMigrationOptions)).Get<DbMigrationOptions>();

                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (_config.ApplyDbMigration && context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                    }

                    if (_config.ApplySeed)
                        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while migrating or seeding the database.");
            }

            try
            {
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }



        }

        private static IConfiguration GetConfiguration(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            configurationBuilder.AddCommandLine(args);
            configurationBuilder.AddEnvironmentVariables();

            return configurationBuilder.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    var configurationRoot = configApp.Build();

                    var env = hostContext.HostingEnvironment;
                    configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
                    configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    configApp.AddEnvironmentVariables();
                    configApp.AddCommandLine(args);

                    configApp.Build();
                })
                 .UseSerilog((context, services, configuration) =>
                 {
                     configuration
                         .ReadFrom.Configuration(context.Configuration)
                         .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
                         .Enrich.FromLogContext()
                         .Enrich.WithCorrelationId(services);
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options => options.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
               
    }
}
