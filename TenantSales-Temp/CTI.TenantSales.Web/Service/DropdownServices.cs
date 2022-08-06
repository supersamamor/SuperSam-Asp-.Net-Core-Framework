using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.TenantSales.Infrastructure.Data;
using CTI.TenantSales.Core.TenantSales;
using CTI.Common.Data;
using CTI.TenantSales.Web.Areas.Admin.Queries.Users;
using MediatR;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Web.Service
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
        public SelectList GetClassificationList(string id)
        {
            return _context.GetSingle<ClassificationState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetTenantList(string? id)
        {
            return _context.GetSingle<TenantState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code + " - " + e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetThemeList(string id)
        {
            return _context.GetSingle<ThemeState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetLevelList(string? id)
        {
            return _context.GetSingle<LevelState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetProjectList(string? id)
        {
            var project = _context.Project.Include(l=>l.Company).ThenInclude(l => l!.DatabaseConnectionSetup).Where(e => e.Id == id).FirstOrDefault();
            if (project == null)
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
            else
            {
                return new SelectList(new List<SelectListItem> { new() { Value = project.Id, Text =  project.DisplayDescription } }, "Value", "Text", project.Id);
            }
        }
        public SelectList GetRentalTypeList(string id)
        {
            return _context.GetSingle<RentalTypeState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetDatabaseConnectionSetupList(string id)
        {
            return _context.GetSingle<DatabaseConnectionSetupState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetBusinessUnitList(string id)
        {
            return _context.GetSingle<BusinessUnitState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetCompanyList(string id)
        {
            var company = _context.Company.Include(l => l.DatabaseConnectionSetup).Where(e => e.Id == id).FirstOrDefault();
            if (company == null)
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
            else
            {
                return new SelectList(new List<SelectListItem> { new() { Value = company.Id, Text = company.DisplayDescription } }, "Value", "Text", company.Id);
            }
        }
        public SelectList GetTenantPOSList(string id)
        {
            return _context.GetSingle<TenantPOSState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public async Task<SelectList> GetSalesCategoryList(string? tenantId, string? salesCategoryCode)
        {
            var query = _context.SalesCategory.Where(e => e.Code == salesCategoryCode).AsNoTracking();
            if (!string.IsNullOrEmpty(tenantId))
            {
                query = query.Where(l=>l.TenantId == tenantId);
            }
            var salesCategory = await query.FirstOrDefaultAsync();
            if (salesCategory == null)
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
            else
            {
                return new SelectList(new List<SelectListItem> { new() { Value = salesCategory.Code, Text = salesCategory.Code } }, "Value", "Text", salesCategory.Code);
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetUserList(string currentSelectedApprover, IList<string> allSelectedApprovers)
        {
            return (await _mediaTr.Send(new GetApproversQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
        }


        public IEnumerable<SelectListItem> YearList
        {
            get
            {
                IList<SelectListItem> items = new List<SelectListItem>();
                var currentYear = DateTime.Today.Year;
                for (int i = DateTime.Today.Year; i >= currentYear - 10; i--)
                {
                    items.Add(new SelectListItem { Text = (i).ToString(), Value = (i).ToString(), Selected = (i == DateTime.Today.Year) });
                }
                return items;
            }
        }

        public IEnumerable<SelectListItem> Month
        {
            get
            {
                IList<SelectListItem> items = new List<SelectListItem>();
                for (int i = 0; i < 12; i++)
                {
                    items.Add(new SelectListItem { Text = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i], Value = (i).ToString(), Selected = (i == DateTime.Today.Month - 1) });
                }

                return items;
            }
        }

        public IEnumerable<SelectListItem> WorkWeek(int year)
        {
            if (year == 0) { year = DateTime.Today.Year; }

            IList<SelectListItem> items = new List<SelectListItem>();
            DateTime startDate = new(year, 1, 1);
            startDate = startDate.AddDays(1 - (int)startDate.DayOfWeek);
            DateTime endDate = startDate.AddDays(6);
            int weekNo = 1;
            while (startDate.Year < 1 + year)
            {
                items.Add(new SelectListItem
                {
                    Text = string.Format("Week " + weekNo + ": From {0:MM/dd/yyyy} to {1:MM/dd/yyyy}", startDate, endDate),
                    Value = string.Format("{0:MM/dd/yyyy}", startDate)
                });
                startDate = startDate.AddDays(7);
                endDate = endDate.AddDays(7);
                weekNo++;
                if (weekNo >= 53) { break; };
            }
            return items;
        }

    }
}
