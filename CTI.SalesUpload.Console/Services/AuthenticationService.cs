using CTI.SalesUpload.Console.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CTI.SalesUpload.Console.Services
{
    public class AuthenticationService
    { 
        public async Task<string> GetAccessToken()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["AuthenticationUrl"]);            
            var request = new RestRequest(new Uri(ConfigurationManager.AppSettings["AuthenticationUrl"]), Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", ConfigurationManager.AppSettings["GrantType"]);
            request.AddParameter("client_id", ConfigurationManager.AppSettings["ClientId"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["ClientSecret"]);
            request.AddParameter("scope", ConfigurationManager.AppSettings["Scope"]);        
            var result = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<TokenModel>(result.Content).AccessToken;
        }
    }
}
