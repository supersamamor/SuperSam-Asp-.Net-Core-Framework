using System;
using System.Text.Json;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions
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