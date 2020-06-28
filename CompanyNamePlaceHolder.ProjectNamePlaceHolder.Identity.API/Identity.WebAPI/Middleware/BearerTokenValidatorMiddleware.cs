using Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Identity.WebAPI.Middleware
{
    public class BearerTokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        public BearerTokenValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IdentityContext dbContext)
        {
            var apiKey = context.Request.Headers["ApiKey"].ToString();
            var apiSecret = context.Request.Headers["Authorization"].ToString().Replace("ApiSecret", "").Trim();
            var api = await dbContext.ProjectNamePlaceHolderIdentityApiClient
                .Where(l => l.Key == apiKey && l.Secret == apiSecret && l.Active == true).FirstOrDefaultAsync();

            //This is temporary.Eventually, authentication will be handled by API gateway
            if (api == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            await _next.Invoke(context);
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBearerTokenValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BearerTokenValidatorMiddleware>();
        }
    }
}
