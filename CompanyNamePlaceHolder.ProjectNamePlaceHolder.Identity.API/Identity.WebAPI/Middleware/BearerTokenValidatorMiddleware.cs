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
            var tokenFromHeader = (context.Request.Headers["Authorization"]).ToString().Replace("Bearer", "").Trim();
            var token = await dbContext.ProjectNamePlaceHolderIdentityApiClient
                    .Where(l => l.Token == tokenFromHeader).FirstOrDefaultAsync();

            //This is temporary.Eventually, authentication will be handled by API gateway
            if (token != null)
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
