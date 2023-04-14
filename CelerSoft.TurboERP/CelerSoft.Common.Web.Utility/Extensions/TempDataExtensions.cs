using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace CelerSoft.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for the <see cref="ITempDataDictionary"/> class
/// </summary>
public static class TempDataExtensions
{
    /// <summary>
    /// Puts an object into the TempData by first serializing it to JSON.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tempData"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// Gets an object from the TempData by deserializing it from JSON.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tempData"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out var o);
        return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
    }
}