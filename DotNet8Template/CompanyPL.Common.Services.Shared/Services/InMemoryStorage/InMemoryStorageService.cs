using Newtonsoft.Json;
using System.Collections.Concurrent;
namespace CompanyPL.Common.Services.Shared.Services.InMemoryStorage
{
    public class InMemoryStorageService
    {
        private readonly ConcurrentDictionary<string, string> refreshTokenStore = new();
        public T? GetRefreshToken<T>(string tokenKey) where T : class
        {
            if (refreshTokenStore.TryGetValue(tokenKey, out var refreshToken))
            {
                return JsonConvert.DeserializeObject<T>(refreshToken);
            }
            return null;
        }
        public void SaveRefreshToken<T>(string tokenKey, T jwToken)
        {
            var jwTokenString = JsonConvert.SerializeObject(jwToken);
            refreshTokenStore.AddOrUpdate(tokenKey, jwTokenString, (key, value) => jwTokenString);          
        }
        public void RemoveRefreshTokenAsync(string tokenKey)
        {
            refreshTokenStore.TryRemove(tokenKey, out _);
        }
    }
}
