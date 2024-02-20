using CompanyNamePlaceHolder.Common.Web.Utility.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity;

public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
                                            RoleManager<ApplicationRole> roleManager,
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
