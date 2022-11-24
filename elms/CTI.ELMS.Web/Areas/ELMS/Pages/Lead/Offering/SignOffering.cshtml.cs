using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead.Offering;

[Authorize(Policy = Permission.Offering.SignOffersheet)]
public class SignOfferingModel : BasePageModel<SignOfferingModel>
{
    [BindProperty]
    public OfferingViewModel Offering { get; set; } = new();
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string id)
    {
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByOfferingIdQuery(id, Constants.TabNavigation.Offerings)));
        return await PageFrom(async () => await Mediatr.Send(new GetOfferingByIdQuery(id)), Offering);
    }
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(Offering.LeadID!, Constants.TabNavigation.Offerings)));
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<SignOfferingCommand>(Offering)), "OfferingDetails", true);
    }
}
