using System.Text.Json;

namespace CTI.Common.Utility.Extensions;

/// <summary>
/// Extension methods for <see cref="object"/>.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Converts an object to a <see cref="Dictionary{TKey, TValue}"/>.
    /// The keys will be the property names of the class.
    /// </summary>
    /// <typeparam name="TValue">Type of the class properties</typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Dictionary<string, TValue>? ToDictionary<TValue>(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, TValue>>(json);
        return dictionary;
    }
}