using CTI.FAS.Application.Features.FAS.Batch.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Batch;

[Authorize]
public class DropdownModel : BasePageModel<DropdownModel>
{
    public BatchViewModel Batch { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request, string companyId, string paymentStatus)
    {
        var query = request.ToQuery<GetBatchQuery>(nameof(BatchState.Batch));
        query.CompanyId = companyId;
        query.PaymentStatus = paymentStatus;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text =  e?.BatchNumber! }));
    }
}
