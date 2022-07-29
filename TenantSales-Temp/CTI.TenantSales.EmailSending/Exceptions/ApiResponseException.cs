namespace CTI.TenantSales.EmailSending.Exceptions
{
    public class ApiResponseException : Exception
    {
        public string? ApiResponse { get; set; } = "";
        public ApiResponseException()
        {
        }

        public ApiResponseException(string? message, HttpResponseMessage response, string? apiResult = "") : base(message == null || message == "" ? response.ToString() : message)
        {
            ApiResponse = apiResult;
        }
        public ApiResponseException(string? message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
