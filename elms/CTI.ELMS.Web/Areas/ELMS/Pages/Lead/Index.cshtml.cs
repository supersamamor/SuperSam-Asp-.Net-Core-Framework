using CTI.ELMS.Application.Features.ELMS.Lead.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Lead.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public LeadViewModel Lead { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {

        var result = await Mediatr.Send(DataRequest!.ToQuery<GetLeadQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.ClientType,
                e.Brand,
                e.Company,
                e.CreatedBy,
                e.CreatedDate,
                e.LastModifiedDate,
                DisplayName = e.DisplayName + "<br /><i>" + e.CreatedBy + "</i>",               
                e.ContactNumber,
                LatestUpdatedDateString = e.LatestUpdatedDate.ToString("MMM dd, yyyy") + "<br /><i>" + e.LatestUpdatedByUsername + "</i>",             
                ProspectAging = e.CreatedDate.ToString("MMM dd, yyyy") + "<br /><i>" + e.LeadAging + " day(s)" + "</i>",
                EmailLink = string.IsNullOrEmpty(e.Email)
                    ? @"<a href=""#"" title=""No Available Email"" style=""color:grey;""><i class=""fas fa-envelope""></i></a>"
                    : @"<a href=""mailto:" + e.Email + @"?subject=" + WebConstants.DefaultLeadMailSubject + @""" title=""" + e.Email + @"""><i class=""fas fa-envelope""></i></a>",
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetLeadQuery>(nameof(LeadState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
