using CTI.Common.Web.Utility.Extensions;
using CTI.LocationApi.Web.Areas.Admin.Models;
using CTI.LocationApi.Web.Areas.Admin.Queries.AuditTrail;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CTI.LocationApi.Core.Identity;

namespace CTI.LocationApi.Web.Areas.Admin.Pages.AuditTrail;

[Authorize(Policy = Permission.AuditTrail.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    readonly UserManager<ApplicationUser> _userManager;

    public DetailsModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public AuditLogViewModel AuditLog { get; set; } = new();

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetAuditLogByIdQuery((int)id)).ToActionResult(
            someAsync: async e =>
            {
                Mapper.Map(e, AuditLog);
                var user = await _userManager.FindByIdAsync(e.UserId);
                AuditLog.User = Mapper.Map<AuditLogUserViewModel>(user);
                return Page();
            },
            none: null);
    }
}
