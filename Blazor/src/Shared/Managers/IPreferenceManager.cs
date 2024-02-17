using ProjectNamePlaceHolder.Shared.Settings;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Shared.Wrapper;

namespace ProjectNamePlaceHolder.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}