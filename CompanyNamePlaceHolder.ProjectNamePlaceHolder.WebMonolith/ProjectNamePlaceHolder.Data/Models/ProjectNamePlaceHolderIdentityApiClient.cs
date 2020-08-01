using System;

namespace ProjectNamePlaceHolder.Data.Models
{
    public class ProjectNamePlaceHolderIdentityApiClient : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
