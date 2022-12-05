using CTI.FAS.Application.Features.FAS.Company.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Company;

[Authorize]
public class DropdownModel : BasePageModel<DropdownModel>
{  
    public IActionResult OnGet()
    {
        return Page();
    }   

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCompanyQuery>(nameof(CompanyState.Name)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e?.EntityDisplayDescription == null ? "N/A" : e?.EntityDisplayDescription! }));
    }
}
