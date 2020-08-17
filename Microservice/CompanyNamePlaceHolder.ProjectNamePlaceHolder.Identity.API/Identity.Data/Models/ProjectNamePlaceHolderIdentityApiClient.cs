using System;

namespace Identity.Data.Models
{
    public class ProjectNamePlaceHolderIdentityApiClient : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
