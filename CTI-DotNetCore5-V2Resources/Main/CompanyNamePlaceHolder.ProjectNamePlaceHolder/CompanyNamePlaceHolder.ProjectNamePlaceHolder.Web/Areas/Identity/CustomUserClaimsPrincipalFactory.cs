using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
                                                RoleManager<IdentityRole> roleManager,
                                                IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            ((ClaimsIdentity)principal.Identity!).AddClaims(new Claim[] {
                new(CustomClaimTypes.Entity, user.EntityId!),
            });
            return principal;
        }

        protected override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            return base.GenerateClaimsAsync(user);
        }
    }
}
