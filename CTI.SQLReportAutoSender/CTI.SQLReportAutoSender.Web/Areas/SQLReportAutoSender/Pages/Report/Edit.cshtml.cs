using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Queries;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Queries;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.Report;

[Authorize(Policy = Permission.Report.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportViewModel Report { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetReportByIdQuery(id)), Report);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportCommand>(Report)), "Details", true);
    }	
	public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddReportDetail")
		{
			return AddReportDetail();
		}
		if (AsyncAction == "RemoveReportDetail")
		{
			return RemoveReportDetail();
		}
		if (AsyncAction == "AddMailRecipient")
		{
			return AddMailRecipient();
		}
		if (AsyncAction == "RemoveMailRecipient")
		{
			return RemoveMailRecipient();
		}
		if (AsyncAction == "AddCustomSchedule")
		{
			return AddCustomSchedule();
		}
		if (AsyncAction == "RemoveCustomSchedule")
		{
			return RemoveCustomSchedule();
		}
		await RefreshScheduleParameters();
		return Partial("_InputFieldsPartial", Report);
    }
	
	private IActionResult AddReportDetail()
	{
		ModelState.Clear();
		if (Report!.ReportDetailList == null) { Report!.ReportDetailList = new List<ReportDetailViewModel>(); }
		Report!.ReportDetailList!.Add(new ReportDetailViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportDetail()
	{
		ModelState.Clear();
		Report.ReportDetailList = Report!.ReportDetailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddMailRecipient()
	{
		ModelState.Clear();
		if (Report!.MailRecipientList == null) { Report!.MailRecipientList = new List<MailRecipientViewModel>(); }
		Report!.MailRecipientList!.Add(new MailRecipientViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveMailRecipient()
	{
		ModelState.Clear();
		Report.MailRecipientList = Report!.MailRecipientList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddCustomSchedule()
	{
		ModelState.Clear();
		if (Report!.CustomScheduleList == null) { Report!.CustomScheduleList = new List<CustomScheduleViewModel>(); }
		Report!.CustomScheduleList!.Add(new CustomScheduleViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveCustomSchedule()
	{
		ModelState.Clear();
		Report.CustomScheduleList = Report!.CustomScheduleList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}
	private async Task RefreshScheduleParameters()
	{
		if (!string.IsNullOrEmpty(Report?.ScheduleFrequencyId))
		{
			Report!.ReportScheduleSettingList = new List<ReportScheduleSettingViewModel>();
			var query = new GetScheduleFrequencyParameterSetupQuery() { ScheduleFrequencyId = Report!.ScheduleFrequencyId };
			foreach (var item in (await Mediatr.Send(query)).Data.ToList())
			{
				Report!.ReportScheduleSettingList!.Add(new ReportScheduleSettingViewModel()
				{
					ReportId = Report.Id,
					ScheduleFrequencyId = item.ScheduleFrequencyId,
					ScheduleParameterId = item.ScheduleParameterId,
					ForeignKeyScheduleFrequency = item.ScheduleFrequency!.Description,
					ForeignKeyScheduleParameter = item.ScheduleParameter!.Description,
				});
			}
		}
	}
}
