using CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.ELMS.Web.Areas.ELMS.Pages.LeadTaskNextStep;

[Authorize(Policy = Permission.LeadTaskNextStep.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public LeadTaskNextStepViewModel LeadTaskNextStep { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetLeadTaskNextStepQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                LeadTaskId = e.LeadTask?.LeadTaskName,
				NextStepId = e.NextStep?.NextStepTaskName,
				PCTDay = e.PCTDay?.ToString("##,##"),
				ClientFeedbackId = e.ClientFeedback?.ClientFeedbackName,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetLeadTaskNextStepQuery>(nameof(LeadTaskNextStepState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
