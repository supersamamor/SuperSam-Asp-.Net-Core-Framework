using System;
using System.Collections.Generic;
using System.Text;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Configurations
{
    public class ApiOptions
    {
        public string IdentityServerBaseUrl { get; set; }
        public string OidcApiName { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
