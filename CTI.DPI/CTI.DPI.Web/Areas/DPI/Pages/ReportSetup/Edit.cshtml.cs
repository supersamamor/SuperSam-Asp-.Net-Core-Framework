using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportSetup;

[Authorize(Policy = Permission.ReportSetup.Edit)]
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
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddReportTable")
		{
			return AddReportTable();
		}
		if (AsyncAction == "RemoveReportTable")
		{
			return RemoveReportTable();
		}
		if (AsyncAction == "AddReportTableJoinParameter")
		{
			return AddReportTableJoinParameter();
		}
		if (AsyncAction == "RemoveReportTableJoinParameter")
		{
			return RemoveReportTableJoinParameter();
		}
		if (AsyncAction == "AddReportColumnHeader")
		{
			return AddReportColumnHeader();
		}
		if (AsyncAction == "RemoveReportColumnHeader")
		{
			return RemoveReportColumnHeader();
		}
		if (AsyncAction == "AddReportFilterGrouping")
		{
			return AddReportFilterGrouping();
		}
		if (AsyncAction == "RemoveReportFilterGrouping")
		{
			return RemoveReportFilterGrouping();
		}
		if (AsyncAction == "AddReportQueryFilter")
		{
			return AddReportQueryFilter();
		}
		if (AsyncAction == "RemoveReportQueryFilter")
		{
			return RemoveReportQueryFilter();
		}
		
		
        return Partial("_InputFieldsPartial", Report);
    }
	
	private IActionResult AddReportTable()
	{
		ModelState.Clear();
		if (Report!.ReportTableList == null) { Report!.ReportTableList = new List<ReportTableViewModel>(); }
		Report!.ReportTableList!.Add(new ReportTableViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportTable()
	{
		ModelState.Clear();
		Report.ReportTableList = Report!.ReportTableList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddReportTableJoinParameter()
	{
		ModelState.Clear();
		if (Report!.ReportTableJoinParameterList == null) { Report!.ReportTableJoinParameterList = new List<ReportTableJoinParameterViewModel>(); }
		Report!.ReportTableJoinParameterList!.Add(new ReportTableJoinParameterViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportTableJoinParameter()
	{
		ModelState.Clear();
		Report.ReportTableJoinParameterList = Report!.ReportTableJoinParameterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddReportColumnHeader()
	{
		ModelState.Clear();
		if (Report!.ReportColumnHeaderList == null) { Report!.ReportColumnHeaderList = new List<ReportColumnHeaderViewModel>(); }
		Report!.ReportColumnHeaderList!.Add(new ReportColumnHeaderViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportColumnHeader()
	{
		ModelState.Clear();
		Report.ReportColumnHeaderList = Report!.ReportColumnHeaderList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddReportFilterGrouping()
	{
		ModelState.Clear();
		if (Report!.ReportFilterGroupingList == null) { Report!.ReportFilterGroupingList = new List<ReportFilterGroupingViewModel>(); }
		Report!.ReportFilterGroupingList!.Add(new ReportFilterGroupingViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportFilterGrouping()
	{
		ModelState.Clear();
		Report.ReportFilterGroupingList = Report!.ReportFilterGroupingList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}

	private IActionResult AddReportQueryFilter()
	{
		ModelState.Clear();
		if (Report!.ReportQueryFilterList == null) { Report!.ReportQueryFilterList = new List<ReportQueryFilterViewModel>(); }
		Report!.ReportQueryFilterList!.Add(new ReportQueryFilterViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportQueryFilter()
	{
		ModelState.Clear();
		Report.ReportQueryFilterList = Report!.ReportQueryFilterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}
	
}
