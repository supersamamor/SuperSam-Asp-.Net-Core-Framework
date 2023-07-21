using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLHealthDeclaration;

[Authorize(Policy = Permission.PLHealthDeclaration.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public PLHealthDeclarationViewModel PLHealthDeclaration { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetPLHealthDeclarationQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.PLVaccine,
				PLIsVaccinated =  e.PLIsVaccinated == true ? "Yes" : "No",
				PLEmployeeId = e.PLEmployee?.PLEmployeeCode,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetPLHealthDeclarationQuery>(nameof(PLHealthDeclarationState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
