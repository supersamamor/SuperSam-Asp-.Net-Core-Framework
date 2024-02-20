using System;
using System.Diagnostics;
using CTI.Common.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;

namespace Identity.WebAPI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            SelfLog.Enable(msg => Debug.WriteLine(msg));
            Log.Logger = SerilogHelper.GetDefaultLoggerConfiguration().CreateLogger();
            try
            {
                CreateWebHostBuilder(args)
               .UseSerilog()
               .Build()
               .Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
