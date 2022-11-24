using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTI.ELMS.Web.Helper;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead.AwardNotice;

[Authorize(Policy = Permission.Offering.View)]
public class AwardNoticeListModel : BasePageModel<AwardNoticeListModel>
{
    public OfferingViewModel Offering { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    public string LeadId { get; set; } = "";
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();

    public async Task<IActionResult> OnGet(string leadId)
    {
        LeadId = leadId;
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(leadId, Constants.TabNavigation.AwardNotice)));
        return Page();
    }


    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetOfferingQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ProjectID = e.Project?.Id,
				e.OfferingHistoryID,
				e.Status,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetOfferingQuery>(nameof(OfferingState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
