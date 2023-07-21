using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLContactInformation;

[Authorize(Policy = Permission.PLContactInformation.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public PLContactInformationViewModel PLContactInformation { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetPLContactInformationQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.PLContactDetails,
				PLEmployeeId = e.PLEmployee?.PLEmployeeCode,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetPLContactInformationQuery>(nameof(PLContactInformationState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
