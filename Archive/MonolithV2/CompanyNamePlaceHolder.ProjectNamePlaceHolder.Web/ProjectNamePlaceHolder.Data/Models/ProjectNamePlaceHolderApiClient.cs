using System;

namespace ProjectNamePlaceHolder.Data.Models
{
    public class ProjectNamePlaceHolderApiClient : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
