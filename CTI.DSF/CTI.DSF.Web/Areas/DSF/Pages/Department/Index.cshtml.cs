using CTI.DSF.Application.Features.DSF.Department.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Department;

[Authorize(Policy = Permission.Department.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public DepartmentViewModel Department { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDepartmentQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                CompanyCode = e.Company?.CompanyCode,
				e.DepartmentCode,
				e.DepartmentName,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDepartmentQuery>(nameof(DepartmentState.DepartmentCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.DepartmentCode }));
    }
}
