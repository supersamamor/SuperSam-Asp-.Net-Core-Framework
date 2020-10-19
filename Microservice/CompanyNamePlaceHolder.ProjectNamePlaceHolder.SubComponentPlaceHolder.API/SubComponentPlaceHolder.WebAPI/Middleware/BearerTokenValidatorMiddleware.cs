using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SubComponentPlaceHolder.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SubComponentPlaceHolder.WebAPI.Middleware
{
    public class BearerTokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        public BearerTokenValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SubComponentPlaceHolderContext dbContext)
        {
            var token = await dbContext.SubComponentPlaceHolderApiClient
                 .Where(l => "Bearer " + l.Token == (context.Request.Headers["Authorization"]).ToString()).FirstOrDefaultAsync();
            if (token == null)
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
