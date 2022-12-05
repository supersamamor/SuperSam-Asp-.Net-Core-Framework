using CTI.FAS.Application.Features.FAS.Creditor.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Creditor;

[Authorize]
public class DropdownModel : BasePageModel<DropdownModel>
{  
    public IActionResult OnGet()
    {
        return Page();
    }

	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request,string companyId)
    {
        var query = request.ToQuery<GetCreditorQuery>(nameof(CreditorState.PayeeAccountName));
        query.CompanyId = companyId;
        query.ExcludeEnrolled = true;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e?.CreditorDisplayDescription == null ? "N/A" : e?.CreditorDisplayDescription! }));
    }
}
