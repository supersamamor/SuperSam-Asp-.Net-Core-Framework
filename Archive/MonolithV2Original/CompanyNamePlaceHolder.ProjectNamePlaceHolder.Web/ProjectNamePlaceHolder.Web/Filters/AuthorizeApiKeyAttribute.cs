using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ProjectNamePlaceHolder.Data;
using System;
using System.Linq;
namespace ProjectNamePlaceHolder.Web.Filters
{
    public class AuthorizeApiKeyAttribute : Attribute, IAuthorizationFilter
    { 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ProjectNamePlaceHolderContext>();          
            var token = dbContext.ProjectNamePlaceHolderApiClient
             .Where(l => "Bearer " + l.Token == (context.HttpContext.Request.Headers["Authorization"]).ToString()).FirstOrDefault();
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }               
        }
    }
}
