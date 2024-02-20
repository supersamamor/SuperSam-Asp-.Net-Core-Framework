using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CTI.Common.Web.Utility.Logging;

/// <summary>
/// A middleware for enriching the Serilog logs.
/// </summary>
public class LogEnricherMiddleware : IMiddleware
{
    readonly IDiagnosticContext DiagnosticContext;

    /// <summary>
    /// Initializes an instance of <see cref="LogEnricherMiddleware"/>
    /// </summary>
    /// <param name="diagnosticContext"></param>
    public LogEnricherMiddleware(IDiagnosticContext diagnosticContext)
    {
        DiagnosticContext = diagnosticContext;
    }

    /// <summary>
    /// Request handling method.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        DiagnosticContext.Set("RouteData", context.GetRouteData(), true);
        await LogEnricher.EnrichFromRequest(DiagnosticContext, context);
        await next(context);
    }
}

/// <summary>
/// Extension methods for enabling Serilog log enrichment in <see cref="IApplicationBuilder"/>
/// and <see cref="IServiceCollection"/>.
/// </summary>
public static class LogEnricherMiddlewareExtensions
{
    /// <summary>
    /// Adds <see cref="LogEnricherMiddleware"/> to the request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder EnrichDiagnosticContext(this IApplicationBuilder app) =>
        app.UseMiddleware<LogEnricherMiddleware>();

    /// <summary>
    /// Registers <see cref="LogEnricherMiddleware"/> in the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddLogEnricherServices(this IServiceCollection services) =>
        services.AddTransient<LogEnricherMiddleware>();
}
