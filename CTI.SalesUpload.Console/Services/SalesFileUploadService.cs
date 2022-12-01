using System.Net.Http;
using System.Threading;
using System.IO;
using System.Configuration;

namespace CTI.SalesUpload.Console.Services
{
    public class SalesFileUploadService
    {
        private readonly HttpClient _client;
        public SalesFileUploadService(HttpClient client)
        {
            _client = client;
        }

        public void Upload(string filePath, string accessToken, CancellationToken token)
        {
            using (var content = new ByteArrayContent(File.ReadAllBytes(filePath)))
            {
                using (var form = new MultipartFormDataContent())
                {

                    _client.DefaultRequestHeaders.Clear();
                    form.Add(content, "FileUpload", Path.GetFileName(filePath));
                    form.Add(new StringContent(ConfigurationManager.AppSettings["IntegrationId"]), "IntegrationId");                  
                    _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
                    using (var response = _client.PostAsync($"/api/v{ConfigurationManager.AppSettings["ApiVersion"]}/TenantPOSSales/FileUpload", form, token))
                    {
                        response.Result.EnsureSuccessStatusCode();
                    }                        
                }
            }
        }
    }
}
