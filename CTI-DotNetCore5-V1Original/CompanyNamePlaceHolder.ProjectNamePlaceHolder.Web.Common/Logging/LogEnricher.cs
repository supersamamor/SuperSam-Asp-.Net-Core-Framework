using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Logging
{
    public static class LogEnricher
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
            diagnosticContext.Set("Path", httpContext.Request.Path.ToString());
            diagnosticContext.Set("Method", httpContext.Request.Method.ToString());
            diagnosticContext.Set("QueryString", httpContext.Request.QueryString.ToString());
            diagnosticContext.Set("Query", httpContext.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()));
            diagnosticContext.Set("Headers", httpContext.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()));
            diagnosticContext.Set("Cookies", httpContext.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()));
            diagnosticContext.Set("Form", httpContext.User?.Claims);

            if (httpContext.Request.ContentType != null
                && httpContext.Request.ContentType.StartsWith("multipart/form-data", StringComparison.InvariantCultureIgnoreCase))
            {
                diagnosticContext.Set("Form", httpContext.Request.Form);
                diagnosticContext.Set("FormFiles", httpContext.Request.Form.Files);
            }
            else
            {
                if (httpContext.Request.ContentLength.HasValue && httpContext.Request.ContentLength > 0)
                {
                    httpContext.Request.EnableBuffering();
                    httpContext.Request.Body.Position = 0;
                    using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                        diagnosticContext.Set("Form", reader.ReadToEndAsync().Result);
                    httpContext.Request.Body.Position = 0;
                }
            }

            diagnosticContext.Set("TraceId", Activity.Current?.Id ?? httpContext?.TraceIdentifier);
        }
    }
}
