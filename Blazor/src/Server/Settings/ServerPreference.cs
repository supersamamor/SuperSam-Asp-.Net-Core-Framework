using System.Linq;
using ProjectNamePlaceHolder.Shared.Constants.Localization;
using ProjectNamePlaceHolder.Shared.Settings;

namespace ProjectNamePlaceHolder.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}