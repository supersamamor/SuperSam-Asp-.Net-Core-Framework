using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.Common.Data;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Queries.Roles;

namespace CTI.SQLReportAutoSender.Web.Service
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
        public SelectList GetReportList(string id)
        {
            return _context.GetSingle<ReportState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetScheduleFrequencyList(string id)
        {
            return _context.GetSingle<ScheduleFrequencyState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetScheduleParameterList(string id)
        {
            return _context.GetSingle<ScheduleParameterState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
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
        public IEnumerable<SelectListItem> Days()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Monday", Value = "Monday", });
            items.Add(new SelectListItem { Text = "Tuesday", Value = "Tuesday", });
            items.Add(new SelectListItem { Text = "Wednesday", Value = "Wednesday", });
            items.Add(new SelectListItem { Text = "Thursday", Value = "Thursday", });
            items.Add(new SelectListItem { Text = "Friday", Value = "Friday", });
            items.Add(new SelectListItem { Text = "Saturday", Value = "Saturday", });
            items.Add(new SelectListItem { Text = "Sunday", Value = "Sunday", });
            return items;
        }
        public IEnumerable<SelectListItem> TimeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1:00 AM", Value = "1:00:00 AM", });
            items.Add(new SelectListItem { Text = "2:00 AM", Value = "2:00:00 AM", });
            items.Add(new SelectListItem { Text = "3:00 AM", Value = "3:00:00 AM", });
            items.Add(new SelectListItem { Text = "4:00 AM", Value = "4:00:00 AM", });
            items.Add(new SelectListItem { Text = "5:00 AM", Value = "5:00:00 AM", });
            items.Add(new SelectListItem { Text = "6:00 AM", Value = "6:00:00 AM", });
            items.Add(new SelectListItem { Text = "7:00 AM", Value = "7:00:00 AM", });
            items.Add(new SelectListItem { Text = "8:00 AM", Value = "8:00:00 AM", });
            items.Add(new SelectListItem { Text = "9:00 AM", Value = "9:00:00 AM", });
            items.Add(new SelectListItem { Text = "10:00 AM", Value = "10:00:00 AM", });
            items.Add(new SelectListItem { Text = "11:00 AM", Value = "11:00:00 AM", });
            items.Add(new SelectListItem { Text = "12:00 AM", Value = "12:00:00 AM", });
            items.Add(new SelectListItem { Text = "1:00 PM", Value = "1:00:00 PM", });
            items.Add(new SelectListItem { Text = "2:00 PM", Value = "2:00:00 PM", });
            items.Add(new SelectListItem { Text = "3:00 PM", Value = "3:00:00 PM", });
            items.Add(new SelectListItem { Text = "4:00 PM", Value = "4:00:00 PM", });
            items.Add(new SelectListItem { Text = "5:00 PM", Value = "5:00:00 PM", });
            items.Add(new SelectListItem { Text = "6:00 PM", Value = "6:00:00 PM", });
            items.Add(new SelectListItem { Text = "7:00 PM", Value = "7:00:00 PM", });
            items.Add(new SelectListItem { Text = "8:00 PM", Value = "8:00:00 PM", });
            items.Add(new SelectListItem { Text = "9:00 PM", Value = "9:00:00 PM", });
            items.Add(new SelectListItem { Text = "10:00 PM", Value = "10:00:00 PM", });
            items.Add(new SelectListItem { Text = "11:00 PM", Value = "11:00:00 PM", });
            items.Add(new SelectListItem { Text = "12:00 PM", Value = "12:00:00 PM", });
            return items;
        }
    }
}
