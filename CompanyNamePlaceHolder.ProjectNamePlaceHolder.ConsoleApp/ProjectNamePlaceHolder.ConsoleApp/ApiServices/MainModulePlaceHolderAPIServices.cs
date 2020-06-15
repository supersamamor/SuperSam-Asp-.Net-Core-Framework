using Newtonsoft.Json;
using ProjectNamePlaceHolder.ConsoleApp.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace ProjectNamePlaceHolder.ConsoleApp.ApiServices
{
    public class MainModulePlaceHolderAPIServices
    {
        private readonly HttpClient _client;

        public MainModulePlaceHolderAPIServices(HttpClient client)
        {
            _client = client;
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
