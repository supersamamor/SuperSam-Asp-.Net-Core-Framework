using CTI.ELMS.Application.Features.ELMS.Activity.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Activity.View)]
public class ActivityListModel : BasePageModel<ActivityListModel>
{
    public ActivityViewModel Activity { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    public string LeadId { get; set; } = "";
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string leadId)
    {
        LeadId = leadId;
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(leadId, Constants.TabNavigation.Activities)));
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {

        var result = await Mediatr.Send(DataRequest!.ToQuery<GetActivityQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ActivityDate = e.ActivityDate?.ToString("MMM dd, yyyy HH:mm"),


                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetActivityQuery>(nameof(ActivityState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
