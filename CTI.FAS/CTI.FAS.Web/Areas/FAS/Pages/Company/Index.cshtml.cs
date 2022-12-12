using CTI.FAS.Application.Features.FAS.Company.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.FAS.Web.Areas.FAS.Pages.Company;

[Authorize(Policy = Permission.Company.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public CompanyViewModel Company { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {

        var result = await Mediatr.Send(DataRequest!.ToQuery<GetCompanyQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                DatabaseConnectionSetupId = e.DatabaseConnectionSetup?.Name,
                e.Name,
                e.Code,
                e.EntityShortName,
                e.SubmitPlace,
                SubmitDeadline = e.SubmitDeadline?.ToString("##,##.00"),
                e.EmailTelephoneNumber,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCompanyQuery>(nameof(CompanyState.Name)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e?.EntityDisplayDescription == null ? "N/A" : e?.EntityDisplayDescription! }));
    }
}
