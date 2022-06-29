using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.SubDetailItemPlaceHolder;

[Authorize(Policy = Permission.SubDetailItemPlaceHolder.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public SubDetailItemPlaceHolderViewModel SubDetailItemPlaceHolder { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetSubDetailItemPlaceHolderQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Code,
								              
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetSubDetailItemPlaceHolderQuery>(nameof(SubDetailItemPlaceHolderState.Code)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Code }));
    }
}
