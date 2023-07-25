using CTI.DPI.Application.Features.DPI.ReportTable.Commands;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportTable;

[Authorize(Policy = Permission.ReportTable.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ReportTableViewModel ReportTable { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddReportTableCommand>(ReportTable)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddReportTableJoinParameter")
		{
			return AddReportTableJoinParameter();
		}
		if (AsyncAction == "RemoveReportTableJoinParameter")
		{
			return RemoveReportTableJoinParameter();
		}
		if (AsyncAction == "AddReportColumnDetail")
		{
			return AddReportColumnDetail();
		}
		if (AsyncAction == "RemoveReportColumnDetail")
		{
			return RemoveReportColumnDetail();
		}
		if (AsyncAction == "AddReportColumnFilter")
		{
			return AddReportColumnFilter();
		}
		if (AsyncAction == "RemoveReportColumnFilter")
		{
			return RemoveReportColumnFilter();
		}
		
		
        return Partial("_InputFieldsPartial", ReportTable);
    }
	
	private IActionResult AddReportTableJoinParameter()
	{
		ModelState.Clear();
		if (ReportTable!.ReportTableJoinParameterList == null) { ReportTable!.ReportTableJoinParameterList = new List<ReportTableJoinParameterViewModel>(); }
		ReportTable!.ReportTableJoinParameterList!.Add(new ReportTableJoinParameterViewModel() { TableId = ReportTable.Id });
		return Partial("_InputFieldsPartial", ReportTable);
	}
	private IActionResult RemoveReportTableJoinParameter()
	{
		ModelState.Clear();
		ReportTable.ReportTableJoinParameterList = ReportTable!.ReportTableJoinParameterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ReportTable);
	}

	private IActionResult AddReportColumnDetail()
	{
		ModelState.Clear();
		if (ReportTable!.ReportColumnDetailList == null) { ReportTable!.ReportColumnDetailList = new List<ReportColumnDetailViewModel>(); }
		ReportTable!.ReportColumnDetailList!.Add(new ReportColumnDetailViewModel() { TableId = ReportTable.Id });
		return Partial("_InputFieldsPartial", ReportTable);
	}
	private IActionResult RemoveReportColumnDetail()
	{
		ModelState.Clear();
		ReportTable.ReportColumnDetailList = ReportTable!.ReportColumnDetailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ReportTable);
	}

	private IActionResult AddReportColumnFilter()
	{
		ModelState.Clear();
		if (ReportTable!.ReportColumnFilterList == null) { ReportTable!.ReportColumnFilterList = new List<ReportColumnFilterViewModel>(); }
		ReportTable!.ReportColumnFilterList!.Add(new ReportColumnFilterViewModel() { TableId = ReportTable.Id });
		return Partial("_InputFieldsPartial", ReportTable);
	}
	private IActionResult RemoveReportColumnFilter()
	{
		ModelState.Clear();
		ReportTable.ReportColumnFilterList = ReportTable!.ReportColumnFilterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ReportTable);
	}
	
}
