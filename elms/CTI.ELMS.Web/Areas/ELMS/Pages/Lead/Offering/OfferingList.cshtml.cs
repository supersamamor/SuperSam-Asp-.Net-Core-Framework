using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTI.ELMS.Web.Helper;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead.Offering;

[Authorize(Policy = Permission.Offering.View)]
public class OfferingListModel : BasePageModel<OfferingListModel>
{
    public OfferingViewModel Offering { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    public string LeadId { get; set; } = "";
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();

    public async Task<IActionResult> OnGet(string leadId)
    {
        LeadId = leadId;
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(leadId, Constants.TabNavigation.Offerings)));
        return Page();
    }


    public async Task<IActionResult> OnPostListAllAsync(string leadId)
    {
        var approvalHelper = new ApprovalHelper(Mediatr);
        var offeringQuery = DataRequest!.ToQuery<GetOfferingQuery>();
        offeringQuery.LeadId = leadId;
        var result = await Mediatr.Send(offeringQuery);
        var userHelper = new UserHelper(Mediatr);
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Project?.ProjectName,
                e.OfferingHistoryID,
                CreatedDate = e.CreatedDate.ToString("MMM dd, yyyy hh:mm tt") + "<br /><i>" + userHelper.GetUserName(e.CreatedBy!) + "</i>",
                StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                Status = OfferingStatusHelper.GetOfferingStatus(e.Status!),
                e.LastModifiedDate,
                e.OfferSheetNo,
                IsSigned = e?.SignedOfferSheetDate != null,
                IsANSigned = e?.SignedAwardNoticeDate != null,
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetOfferingQuery>(nameof(OfferingState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
