using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Application.Features.ELMS.Activity.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Activity.Create)]
public class EditActivityModel : BasePageModel<EditActivityModel>
{
    [BindProperty]
    public ActivityViewModel Activity { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
	public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
	public async Task<IActionResult> OnGet(string id, string leadId)
    {
		LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(leadId, Constants.TabNavigation.Activities)));
        return await PageFrom(async () => await Mediatr.Send(new GetActivityByIdQuery(id)), Activity);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(Activity.LeadID!, Constants.TabNavigation.Activities)));
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddActivityCommand>(Activity)), "EditActivity", true);		
	}	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();	
        return Partial("_InputFieldsPartial", Activity);
    }
}
