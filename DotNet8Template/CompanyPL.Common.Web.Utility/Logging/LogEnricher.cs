using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;
using System.Text;

namespace CompanyPL.Common.Web.Utility.Logging;

/// <summary>
/// Helper methods for enriching the Serilog logs.
/// </summary>
public static class LogEnricher
{
    /// <summary>
    /// Enrich the logs with additional information from the <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="diagnosticContext"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static async Task EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
        diagnosticContext.Set("Path", httpContext.Request.Path.ToString());
        diagnosticContext.Set("Method", httpContext.Request.Method.ToString());
        diagnosticContext.Set("QueryString", httpContext.Request.QueryString.ToString());
        diagnosticContext.Set("Query", httpContext.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()));
        diagnosticContext.Set("Headers", httpContext.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()));
        diagnosticContext.Set("Cookies", httpContext.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()));
        diagnosticContext.Set("Claims", httpContext.User?.Claims);

        if (httpContext.Request.ContentLength.HasValue && httpContext.Request.ContentLength > 0)
        {
            if (httpContext.Request.ContentType!.Contains("form", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var form = httpContext.Request.Form.ToDictionary(form => form.Key, form => form.Value);
                if (form != null && form.ContainsKey("Input.Password"))
                {
                    form["Input.Password"] = "[redacted]";
                }
                if (form != null && form.ContainsKey("Input.ConfirmPassword"))
                {
                    form["Input.ConfirmPassword"] = "[redacted]";
                }
                if (form != null && form.ContainsKey("client_secret"))
                {
                    form["client_secret"] = "[redacted]";
                }
                diagnosticContext.Set("Form", form);
                diagnosticContext.Set("FormFiles", httpContext.Request.Form.Files);
            }
            httpContext.Request.EnableBuffering();
            httpContext.Request.Body.Position = 0;
            using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                var body = await reader.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(body))
                    diagnosticContext.Set("Body", body);
            }
            httpContext.Request.Body.Position = 0;
        }

        diagnosticContext.Set("TraceId", Activity.Current?.Id ?? httpContext?.TraceIdentifier);
    }
}