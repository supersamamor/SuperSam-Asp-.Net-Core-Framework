using System.Text.Json;
using ProjectNamePlaceHolder.Application.Interfaces.Serialization.Options;

namespace ProjectNamePlaceHolder.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}