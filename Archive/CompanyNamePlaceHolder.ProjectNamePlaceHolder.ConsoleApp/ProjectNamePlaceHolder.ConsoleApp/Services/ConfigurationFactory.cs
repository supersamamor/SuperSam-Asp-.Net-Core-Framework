using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;

namespace ProjectNamePlaceHolder.ConsoleApp.Services
{
    public static class ConfigurationFactory
    {
        private static IConfigurationBuilder CreateDefaultBuilder(IConfigurationBuilder builder, IHostingEnvironment env)
        {
            return CreateDefaultBuilder(builder, env.EnvironmentName);
        }

        private static IConfigurationBuilder CreateDefaultBuilder(IConfigurationBuilder builder, string environmentName)
        {
            return builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public static IConfiguration CreateDefaultConfiguration()
        {
            var env = new HostingEnvironment
            {
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
            };

            var builder = CreateDefaultBuilder(new ConfigurationBuilder(), env);
            return builder.Build();
        }

        public static IConfiguration CreateConfiguration(string configFile)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFile, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
