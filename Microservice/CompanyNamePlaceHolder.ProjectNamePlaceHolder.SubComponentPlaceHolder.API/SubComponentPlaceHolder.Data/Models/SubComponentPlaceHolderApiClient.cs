using System;

namespace SubComponentPlaceHolder.Data.Models
{
    public class SubComponentPlaceHolderApiClient : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
