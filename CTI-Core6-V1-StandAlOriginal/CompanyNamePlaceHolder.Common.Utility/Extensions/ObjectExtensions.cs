using System.Collections.Generic;
using System.Text.Json;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, TValue>? ToDictionary<TValue>(this object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, TValue>>(json);
            return dictionary;
        }
    }
}