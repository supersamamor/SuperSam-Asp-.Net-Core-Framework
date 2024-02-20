using Newtonsoft.Json;
using ProjectNamePlaceHolder.ConsoleApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProjectNamePlaceHolder.ConsoleApp.ApiServices
{
    public class MainModulePlaceHolderAPIServices
    {
        private readonly HttpClient _client;

        public MainModulePlaceHolderAPIServices(HttpClient client, string apiKey, string apiSecret)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiSecret", apiSecret);
        }

        public IList<MainModulePlaceHolderModel> GetMainModulePlaceHolderList()
        {
            var response = _client.GetAsync("MainModulePlaceHolder/");
            response.Result.EnsureSuccessStatusCode();
            var result = response.Result.Content.ReadAsStringAsync().Result;
            var record = JsonConvert.DeserializeObject<CustomPagedList<MainModulePlaceHolderModel>>(result);
            return record.Items;
        }
    }
}
