using CTI.SalesUpload.Console.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CTI.SalesUpload.Console.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _client;
        public AuthenticationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<JwToken> GetJwTokenAsync(string authUrl, CancellationToken token)
        {
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", ConfigurationManager.AppSettings["GrantType"]),
                new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["ClientId"]),
                new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["ClientSecret"]),
                new KeyValuePair<string, string>("scope",ConfigurationManager.AppSettings["Scope"])
            };
            var request = new HttpRequestMessage(HttpMethod.Post, authUrl + $"/connect/token") { Content = new FormUrlEncodedContent(headers) };
            var response = await _client.SendAsync(request, token);
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<JwToken>(result);
        }
    }
}
