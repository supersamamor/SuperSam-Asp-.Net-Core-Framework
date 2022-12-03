using CTI.FAS.Application.Features.FAS.Generated.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.FAS.Web.Areas.FAS.Pages.Generated;

[Authorize(Policy = Permission.Generated.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public GeneratedViewModel Generated { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetGeneratedQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                CompanyId = e.Company?.Id,
				CreditorId = e.Creditor?.Id,
				BatchId = e.Batch?.Id,
				e.Name,
				TransmissionDate = e.TransmissionDate.ToString("MMM dd, yyyy HH:mm"),
				e.DocumentNumber,
				DocumentDate = e.DocumentDate.ToString("MMM dd, yyyy HH:mm"),
				DocumentAmount = e.DocumentAmount.ToString("##,##.00"),
				e.CheckNumber,
				e.Release,
				e.PaymentType,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetGeneratedQuery>(nameof(GeneratedState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
