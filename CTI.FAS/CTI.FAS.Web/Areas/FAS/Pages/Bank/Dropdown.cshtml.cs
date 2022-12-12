using CTI.FAS.Application.Features.FAS.Bank.Queries;
using CTI.FAS.Application.Features.FAS.Batch.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Bank;

[Authorize]
public class DropdownModel : BasePageModel<DropdownModel>
{
    public BatchViewModel Batch { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request, string companyId)
    {
        var query = request.ToQuery<GetBankQuery>(nameof(BankState.BankName));
        query.CompanyId = companyId;      
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text =  e?.DisplayName! }));
    }
}
