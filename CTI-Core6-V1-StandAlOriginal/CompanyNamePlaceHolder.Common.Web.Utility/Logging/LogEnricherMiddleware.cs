using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Logging
{
    public class LogEnricherMiddleware : IMiddleware
    {
        readonly IDiagnosticContext DiagnosticContext;

        public LogEnricherMiddleware(IDiagnosticContext diagnosticContext)
        {
            DiagnosticContext = diagnosticContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            DiagnosticContext.Set("RouteData", context.GetRouteData(), true);
            await LogEnricher.EnrichFromRequest(DiagnosticContext, context);
            await next(context);
        }
    }

    public static class LogEnricherMiddlewareExtensions
    {
        public static IApplicationBuilder EnrichDiagnosticContext(this IApplicationBuilder app) =>
            app.UseMiddleware<LogEnricherMiddleware>();

        public static IServiceCollection AddLogEnricherServices(this IServiceCollection services) =>
            services.AddTransient<LogEnricherMiddleware>();
    }
}
