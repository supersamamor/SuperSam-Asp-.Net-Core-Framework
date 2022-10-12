using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTI.SQLReportAutoSender.Web.Helper;


namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.Report;

[Authorize(Policy = Permission.Report.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportViewModel Report { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Description,
				ScheduleFrequencyId = e.ScheduleFrequency?.Description,
				IsActive =  e.IsActive == true ? "Yes" : "No",
				MultipleReportType = e.MultipleReportType.ToString("##,##"),
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportQuery>(nameof(ReportState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
