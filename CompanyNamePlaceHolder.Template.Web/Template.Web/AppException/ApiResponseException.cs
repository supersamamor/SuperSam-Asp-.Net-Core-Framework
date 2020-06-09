using Newtonsoft.Json;
using System;

namespace Template.Web.AppException
{
    public class ApiResponseException : Exception
    {
        public ApiErrorModel Error { get; set; }
        public ApiResponseException(string responseString)
        {
            if (responseString != null)
            {
                Error = JsonConvert.DeserializeObject<ApiErrorModel>(responseString);
                if (Error != null) { return; }
            }
            Error = new ApiErrorModel { Title = Resource.PromptMessageDefaultError, Status = 500, Detail = Resource.PromptMessageDefaultError };          
        }
    }
    public class ApiErrorModel
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
    }
}
