using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions
{
    public static class StringExtensions
    {
        public static string JsonPrettify(this string json)
        {
            try
            {
                var jDoc = JsonDocument.Parse(json);
                return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception)
            {
                return json;
            }
        }
    }
}
