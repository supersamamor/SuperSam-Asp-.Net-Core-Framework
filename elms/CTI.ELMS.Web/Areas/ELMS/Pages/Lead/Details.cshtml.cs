using CTI.ELMS.Application.Features.ELMS.Lead.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Lead.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public LeadViewModel Lead { get; set; } = new();
	[BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(id, Constants.TabNavigation.Info)));
        return await PageFrom(async () => await Mediatr.Send(new GetLeadByIdQuery(id)), Lead);
    }
}
