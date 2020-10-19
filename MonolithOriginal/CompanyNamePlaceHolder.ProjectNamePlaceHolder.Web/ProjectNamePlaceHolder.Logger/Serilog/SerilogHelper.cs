using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace ProjectNamePlaceHolder.Logger
{
    public class SerilogHelper
    {
        public static LoggerConfiguration GetDefaultLoggerConfiguration(IConfiguration configuration = null)
        {
            var config = configuration ?? _configuration;

            return new LoggerConfiguration()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .ReadFrom.Configuration(config)
                            .Enrich.FromLogContext()
                            .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}");
        }

        public static ILogger CreateMSSqlLogger(IConfiguration configuration = null)
        {
            var config = configuration ?? _configuration;

            var options = new ColumnOptions();
            options.Id.NonClusteredIndex = true;
            options.Store.Add(StandardColumn.LogEvent);
            options.Store.Remove(StandardColumn.Properties);
            options.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn("HttpContext", SqlDbType.NVarChar),
                new SqlColumn("Method", SqlDbType.NVarChar),
                new SqlColumn("Path", SqlDbType.NVarChar),
                new SqlColumn("Host", SqlDbType.NVarChar),
                new SqlColumn("RemoteIpAddress", SqlDbType.NVarChar),
                new SqlColumn("UserName", SqlDbType.NVarChar),
                new SqlColumn("CorrelationId", SqlDbType.NVarChar),
                new SqlColumn("RequestId", SqlDbType.NVarChar),
                new SqlColumn("StatusCode", SqlDbType.NVarChar),
                new SqlColumn("Elapsed", SqlDbType.NVarChar)
            };

            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(config)              
              .CreateLogger();

            return logger;
        }

        private static IConfiguration _configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
