using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Application.Features.ELMS.Activity.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Application.Features.ELMS.Unit.Queries;
using CTI.ELMS.Core.ELMS;
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
    [BindProperty]
    public string? AddUnitActivityUnitId { get; set; }
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string id)
    {
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByActivityIdQuery(id, Constants.TabNavigation.Activities)));       
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
    public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "RemoveUnitActivity")
        {
            return RemoveUnitActivity();
        }
        if (AsyncAction == "AddUnitActivity")
        {
            return await AddUnitActivity();
        }
        return Partial("_ActivityInputFieldsPartial", Activity);
    }
    private IActionResult RemoveUnitActivity()
    {
        ModelState.Clear();
        Activity.UnitActivityList = Activity!.UnitActivityList!.Where(l => l.Id != RemoveSubDetailId).ToList();
        return Partial("_ActivityInputFieldsPartial", Activity);
    }
    private async Task<IActionResult> AddUnitActivity()
    {
        ModelState.Clear();
        UnitActivityViewModel unitActivityToAdd = new();
        _ = (await Mediatr.Send(new GetUnitByIdQuery(AddUnitActivityUnitId!))).Select(l => unitActivityToAdd = Mapper.Map<UnitActivityViewModel>(l));
        unitActivityToAdd.ActivityID = Activity.Id;
        if (Activity!.UnitActivityList == null) { Activity!.UnitActivityList = new List<UnitActivityViewModel>(); }
        Activity!.UnitActivityList!.Add(unitActivityToAdd);
        return Partial("_ActivityInputFieldsPartial", Activity);
    }
}
