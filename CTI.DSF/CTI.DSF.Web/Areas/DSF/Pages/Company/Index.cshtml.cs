using CTI.DSF.Application.Features.DSF.Company.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Company;

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
                e.CompanyCode,
				e.CompanyName,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCompanyQuery>(nameof(CompanyState.CompanyName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.CompanyName }));
    }
}
