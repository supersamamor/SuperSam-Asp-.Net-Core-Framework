
using ProjectNamePlaceHolder.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace ProjectNamePlaceHolder.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}