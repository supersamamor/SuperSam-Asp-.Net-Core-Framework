using System.Text.Json;

namespace CompanyPL.Common.Utility.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns formatted version of the JSON.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
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