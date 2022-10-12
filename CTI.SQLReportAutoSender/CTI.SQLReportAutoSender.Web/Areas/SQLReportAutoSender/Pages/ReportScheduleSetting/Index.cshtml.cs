using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.ReportScheduleSetting;

[Authorize(Policy = Permission.ReportScheduleSetting.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportScheduleSettingViewModel ReportScheduleSetting { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportScheduleSettingQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportId = e.Report?.Id,
				ScheduleFrequencyId = e.ScheduleFrequency?.Description,
				ScheduleParameterId = e.ScheduleParameter?.Description,
				e.Value,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportScheduleSettingQuery>(nameof(ReportScheduleSettingState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
