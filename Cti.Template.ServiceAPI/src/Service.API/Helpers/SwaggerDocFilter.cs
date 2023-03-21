using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHANGE_TO_APP_NAME.Services.API.Helpers
{
    public class SwaggerDocFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var _apiVersion = ApiVersion.Parse(swaggerDoc.Info.Version);

            if (_apiVersion.MajorVersion > 1)
            {
                var _verPath = $"v{_apiVersion.MajorVersion}";
                if (_apiVersion.MinorVersion > 0)
                    _verPath += $".{_apiVersion.MinorVersion}";
                if (!string.IsNullOrEmpty(_apiVersion.Status))
                    _verPath += $"-{_apiVersion.Status}";

                var nonVersionedRoutes = swaggerDoc.Paths
                    .Where(x => !x.Key.Contains(_verPath))
                    .ToList();

                nonVersionedRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
            }
        }
    }
}
