using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.FAS.Infrastructure.Data;
using CTI.FAS.Core.FAS;
using CTI.Common.Data;
using CTI.FAS.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.FAS.Web.Areas.Admin.Queries.Roles;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Web.Service
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
        public SelectList GetCompanyList(string id)
        {
            var companyList = _context.Company.Where(e => e.Id == id)
             .Include(l => l.DatabaseConnectionSetup).ToList();
            if (companyList.Count > 0)
            {
                return new SelectList(companyList.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Value = a.Id,
                        Text = a.EntityDisplayDescription,
                    };
                }), "Value", "Text", id);
            }
            else
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
        }
        public SelectList GetCreditorList(string id)
        {
            return _context.GetSingle<CreditorState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.CreditorDisplayDescription } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetDatabaseConnectionSetupList(string id)
        {
            return _context.GetSingle<DatabaseConnectionSetupState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetEnrolledPayeeList(string id)
        {
            return _context.GetSingle<EnrolledPayeeState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetBatchList(string id)
        {
            return _context.GetSingle<BatchState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetProjectList(string id)
        {
            return _context.GetSingle<ProjectState>(e => e.Id == id, new()).Result.Match(
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
        public IEnumerable<SelectListItem> AccountTypeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.AccountType.Checking, Value = Core.Constants.AccountType.Checking, },
                new SelectListItem { Text = Core.Constants.AccountType.Savings, Value = Core.Constants.AccountType.Savings, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> EnrollmentStatusList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.EnrollmentStatus.New, Value = Core.Constants.EnrollmentStatus.New, },
                new SelectListItem { Text = Core.Constants.EnrollmentStatus.Active, Value = Core.Constants.EnrollmentStatus.Active, },
                new SelectListItem { Text = Core.Constants.EnrollmentStatus.InActive, Value = Core.Constants.EnrollmentStatus.InActive, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> PaymentTransactionStatusList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.PaymentTransactionStatus.New, Value = Core.Constants.PaymentTransactionStatus.New, },
                new SelectListItem { Text = Core.Constants.PaymentTransactionStatus.Generated, Value = Core.Constants.PaymentTransactionStatus.Generated, },
                new SelectListItem { Text = Core.Constants.PaymentTransactionStatus.Sent, Value = Core.Constants.PaymentTransactionStatus.Sent, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> PaymentTypeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.PaymentType.CheckPrepare, Value = Core.Constants.PaymentType.CheckPrepare, },
                new SelectListItem { Text = Core.Constants.PaymentType.ESettle, Value = Core.Constants.PaymentType.ESettle, },
            };
            return items;
        }
    }
}
