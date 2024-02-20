using Correlate.AspNetCore;
using ProjectNamePlaceHolder.Logger.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ProjectNamePlaceHolder.Logger.Extensions.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogCorrelation(this IApplicationBuilder builder)
        {
            builder.UseCorrelate();
            builder.UseMiddleware<LogCorrelationMiddleware>();
            builder.UseSerilogRequestLogging();

            return builder;
        }
    }
}
