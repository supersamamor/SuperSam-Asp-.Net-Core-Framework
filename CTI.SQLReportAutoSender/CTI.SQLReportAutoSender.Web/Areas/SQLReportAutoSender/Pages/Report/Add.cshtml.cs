using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Commands;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Pages.Report;

[Authorize(Policy = Permission.Report.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ReportViewModel Report { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		this.Report.MailSettingList = new List<MailSettingViewModel>() { new MailSettingViewModel() { ReportId = this.Report.Id} };
		this.Report.ReportScheduleSettingList = new List<ReportScheduleSettingViewModel>() { new ReportScheduleSettingViewModel() { ReportId = this.Report.Id} };
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddReportCommand>(Report)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
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
	
}
