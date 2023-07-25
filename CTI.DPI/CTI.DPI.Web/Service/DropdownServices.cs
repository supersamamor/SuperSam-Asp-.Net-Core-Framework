using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.DPI.Infrastructure.Data;
using CTI.DPI.Core.DPI;
using CTI.Common.Data;
using CTI.DPI.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.DPI.Web.Areas.Admin.Queries.Roles;

namespace CTI.DPI.Web.Service
{
    public class DropdownServices
    {
		private readonly ApplicationContext _context;
		private readonly IMediator _mediaTr;

		public DropdownServices(ApplicationContext context, IMediator mediaTr)
		{            
			_context = context;
			_mediaTr = mediaTr;
		}       
		public SelectList GetReportTableList(string? id)
		{
			return _context.GetSingle<ReportTableState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetReportColumnHeaderList(string? id)
		{
			return _context.GetSingle<ReportColumnHeaderState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetReportList(string? id)
		{
			return _context.GetSingle<ReportState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.ReportName } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetReportFilterGroupingList(string? id)
		{
			return _context.GetSingle<ReportFilterGroupingState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		
		public async Task<IEnumerable<SelectListItem>> GetUserList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproversQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
public async Task<IEnumerable<SelectListItem>> GetRoleApproverList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproverRolesQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
    }
}
