using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ProjectNamePlaceHolder.Logger.Middleware
{
    public class HttpContextCache
    {
        public string IpAddress { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public bool IsHttps { get; set; }

        public string Scheme { get; set; }

        public string Method { get; set; }

        public string ContentType { get; set; }

        public string Protocol { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> Query { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, string> Cookies { get; set; }

        public string Body { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static HttpContextCache CreateFrom(HttpContext ctx)
        {
            if (ctx == null) return new HttpContextCache();

            var httpContextCache = new HttpContextCache
            {
                IpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                Host = ctx.Request.Host.ToString(),
                Path = ctx.Request.Path.ToString(),
                IsHttps = ctx.Request.IsHttps,
                Scheme = ctx.Request.Scheme,
                Method = ctx.Request.Method,
                ContentType = ctx.Request.ContentType,
                Protocol = ctx.Request.Protocol,
                QueryString = ctx.Request.QueryString.ToString(),
                Query = ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Cookies = ctx.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString())
            };

            if (ctx.Request.ContentLength.HasValue && ctx.Request.ContentLength > 0)
            {
                ctx.Request.EnableBuffering();

                using (StreamReader reader = new StreamReader(ctx.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    httpContextCache.Body = reader.ReadToEndAsync().Result;
                }

                ctx.Request.Body.Position = 0;
            }
            return httpContextCache;
        }
    }
}
