using CTI.ContractManagement.EmailSending.Exceptions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CTI.ContractManagement.EmailSending.Services
{
    public class OneMessageEmailServiceApi : IEmailSender
    {
        private readonly MailSettings _emailApiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        public OneMessageEmailServiceApi(IOptions<MailSettings> emailApiConfig, IHttpClientFactory httpClientFactory)
        {
            _emailApiConfig = emailApiConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", await GetJwTokenAsync(client, new CancellationToken())));
            var emailRequest = new EmailRequest { From = _emailApiConfig.EmailApiSender, To = new List<string> { email }, Text = htmlMessage };
            var content = new StringContent(JsonConvert.SerializeObject(emailRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_emailApiConfig.EmailApiUrl + "/api/InfoBip/sms", content);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                throw new ApiResponseException(result, response);
            }
            await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetJwTokenAsync(HttpClient client, CancellationToken token)
        {
            var response = await client.PostAsync(_emailApiConfig.EmailApiUrl + $"/token?username={_emailApiConfig.EmailApiUsername}&password={_emailApiConfig.EmailApiPassword}", new StringContent(""), token);
            var result = await response.Content.ReadAsStringAsync(token);
            response.EnsureSuccessStatusCode();
            return result;
        }

        private class EmailRequest
        {
            public string? Text { get; set; }
            public List<string>? To { get; set; }
            public string? From { get; set; }
        }
    }
}
