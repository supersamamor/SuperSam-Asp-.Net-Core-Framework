using Correlate;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Logger.Middleware
{
    public class LogCorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public LogCorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, ICorrelationContextAccessor correlationContextAccessor)
        {
            var ctx = HttpContextCache.CreateFrom(context);

            using (LogContext.PushProperty("HttpContext", ctx, true))
            using (LogContext.PushProperty("Method", ctx.Method))
            using (LogContext.PushProperty("Path", ctx.Path))
            using (LogContext.PushProperty("Host", ctx.Host))
            using (LogContext.PushProperty("RemoteIpAddress", ctx.IpAddress))
            using (LogContext.PushProperty("UserName", context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "anonymous"))
            using (LogContext.PushProperty("CorrelationId", correlationContextAccessor.CorrelationContext.CorrelationId))
            using (LogContext.PushProperty("RequestId", context.Request.HttpContext.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}
