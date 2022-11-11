using CTI.ELMS.Application.Features.ELMS.NextStep.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.NextStep;

[Authorize(Policy = Permission.NextStep.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public NextStepViewModel NextStep { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetNextStepQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.NextStepTaskName,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetNextStepQuery>(nameof(NextStepState.NextStepTaskName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.NextStepTaskName }));
    }
    public async Task<IActionResult> OnGetDropdownNextStepAsync(string leadTask, string clientFeedback)
    {
        var list = new List<SelectListItem>();
        if (!string.IsNullOrEmpty(leadTask))
        {
            var nextStepList = (await Mediatr.Send(new GetNextStepQuery() { LeadTaskId = leadTask, ClientFeedbackId = clientFeedback })).Data.ToList();
            foreach (var p in nextStepList)
                list.Add(new SelectListItem { Value = p.Id, Text = p.NextStepTaskName });
        }
        return new JsonResult(list);
    }
}
