using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Oidc.Models
{
    public class OidcApplication : OpenIddictEntityFrameworkCoreApplication<string, OidcAuthorization, OidcToken>
    {
        public string Entity { get; set; } = "";
    }
    public class OidcAuthorization : OpenIddictEntityFrameworkCoreAuthorization<string, OidcApplication, OidcToken> { }
    public class OidcScope : OpenIddictEntityFrameworkCoreScope<string> { }
    public class OidcToken : OpenIddictEntityFrameworkCoreToken<string, OidcApplication, OidcAuthorization> { }
}
