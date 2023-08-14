using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.DSF.Infrastructure.Data;
using CTI.DSF.Core.DSF;
using CTI.Common.Data;
using CTI.DSF.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.DSF.Web.Areas.Admin.Queries.Roles;
using CTI.DSF.Application.Features.DSF.Report.Queries;
using System.Globalization;
using CTI.DSF.Application.Features.DSF.Company.Queries;
using CTI.DSF.Application.Features.DSF.Department.Queries;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Web.Service
{
	public class DropdownServices
	{
		private readonly ApplicationContext _appicationcontext;
        private readonly IdentityContext _identityContext;
        private readonly IMediator _mediaTr;

		public DropdownServices(ApplicationContext ApplicationContext, IMediator mediaTr, IdentityContext IdentityContext)
		{
			_appicationcontext = ApplicationContext;
			_mediaTr = mediaTr;
			_identityContext = IdentityContext;
		}
		public async Task<IEnumerable<SelectListItem>> GetRoleList()
		{
			return (await _mediaTr.Send(new GetRolesQuery())).Data.Select(l => new SelectListItem { Value = l.Name, Text = l.Name });
		}
		public SelectList GetReportTableList(string? id)
		{
			return _appicationcontext.GetSingle<ReportTableState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public IEnumerable<SelectListItem> QueryTypeList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
                //new SelectListItem { Text = Core.Constants.QueryType.QueryBuilder, Value = Core.Constants.QueryType.QueryBuilder, },
                new SelectListItem { Text = Core.Constants.QueryType.TSql, Value = Core.Constants.QueryType.TSql, }
			};
			return items;
		}
		public IEnumerable<SelectListItem> ReportChartTypeList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.ReportChartType.HorizontalBar, Value = Core.Constants.ReportChartType.HorizontalBar, },
				new SelectListItem { Text = Core.Constants.ReportChartType.Pie, Value = Core.Constants.ReportChartType.Pie, },
				new SelectListItem { Text = Core.Constants.ReportChartType.Table, Value = Core.Constants.ReportChartType.Table, },
			};
			return items;
		}
		public IEnumerable<SelectListItem> DataTypeList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.DataTypes.CustomDropdown, Value = Core.Constants.DataTypes.CustomDropdown, },
				new SelectListItem { Text = Core.Constants.DataTypes.Date, Value = Core.Constants.DataTypes.Date, },
				new SelectListItem { Text = Core.Constants.DataTypes.DropdownFromTable, Value = Core.Constants.DataTypes.DropdownFromTable, },
				new SelectListItem { Text = Core.Constants.DataTypes.Months, Value = Core.Constants.DataTypes.Months, },
				new SelectListItem { Text = Core.Constants.DataTypes.Years, Value = Core.Constants.DataTypes.Years, },
			};
			return items;
		}

		public IEnumerable<SelectListItem> TaskClassificationList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.TaskClassifications.Recurring, Value = Core.Constants.TaskClassifications.Recurring },
				new SelectListItem { Text = Core.Constants.TaskClassifications.Adhoc, Value = Core.Constants.TaskClassifications.Adhoc }

			};
			return items;
		}

        public IEnumerable<SelectListItem> TaskFrequencyList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.TaskFrequencies.Annual, Value = Core.Constants.TaskFrequencies.Annual },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.SemiAnnual, Value = Core.Constants.TaskFrequencies.SemiAnnual },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.Quarterly, Value = Core.Constants.TaskFrequencies.Quarterly },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.Monthly, Value = Core.Constants.TaskFrequencies.Monthly },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.Weekly, Value = Core.Constants.TaskFrequencies.Weekly },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.Daily, Value = Core.Constants.TaskFrequencies.Daily },
                new SelectListItem { Text = Core.Constants.TaskFrequencies.OneTime, Value = Core.Constants.TaskFrequencies.OneTime }

            };
            return items;
        }

        public IEnumerable<SelectListItem> TaskFrequencyRecurList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.TaskFrequencies.Annual, Value = Core.Constants.TaskFrequencies.Annual },
				new SelectListItem { Text = Core.Constants.TaskFrequencies.SemiAnnual, Value = Core.Constants.TaskFrequencies.SemiAnnual },
				new SelectListItem { Text = Core.Constants.TaskFrequencies.Quarterly, Value = Core.Constants.TaskFrequencies.Quarterly },
				new SelectListItem { Text = Core.Constants.TaskFrequencies.Monthly, Value = Core.Constants.TaskFrequencies.Monthly },
				new SelectListItem { Text = Core.Constants.TaskFrequencies.Weekly, Value = Core.Constants.TaskFrequencies.Weekly },
				new SelectListItem { Text = Core.Constants.TaskFrequencies.Daily, Value = Core.Constants.TaskFrequencies.Daily },
			
			};
			return items;
		}

        public IEnumerable<SelectListItem> TaskFrequencyAdhocList()
        {
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.TaskFrequencies.OneTime, Value = Core.Constants.TaskFrequencies.OneTime, Selected = true}
            };
            return items;
        }

		// Entity Setup

		public async Task<IEnumerable<SelectListItem>> GetEntities()
		{
			return (await _mediaTr.Send(new GetCompanyQuery())).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.CompanyName });
		}

        // To Delete
        public IEnumerable<SelectListItem> EndorserList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.SampleEndorser.MariaClara, Value =  Core.Constants.SampleEndorser.MariaClara },
				new SelectListItem { Text = Core.Constants.SampleEndorser.MariaJuana, Value =  Core.Constants.SampleEndorser.MariaJuana }
			};
			return items;
		}

		public IEnumerable<SelectListItem> ApproverList()
		{
			IList<SelectListItem> items = new List<SelectListItem>
			{
				new SelectListItem { Text = Core.Constants.SampleApprover.JohnDoe, Value =  Core.Constants.SampleApprover.JohnDoe },
				new SelectListItem { Text = Core.Constants.SampleApprover.JuanDelaCruz, Value =  Core.Constants.SampleApprover.JuanDelaCruz }
			};
			return items;
		}

		public IEnumerable<SelectListItem> GetDropdownFromCsv(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return Enumerable.Empty<SelectListItem>();
			}
			return value.Split(',')
						 .Select(option => new SelectListItem { Text = option.Trim(), Value = option.Trim() })
						 .ToList();
		}
		public IEnumerable<SelectListItem> GetYearsList(int yearsPrevious, int yearsAdvance)
		{
			List<SelectListItem> yearsList = new();
			int currentYear = DateTime.Now.Year;
			int startYear = currentYear - yearsPrevious;
			int endYear = currentYear + yearsAdvance;
			for (int year = startYear; year <= endYear; year++)
			{
				SelectListItem listItem = new()
				{
					Text = year.ToString(),
					Value = year.ToString(),
				};
				yearsList.Add(listItem);
			}
			return yearsList;
		}
		public IEnumerable<SelectListItem> GetMonthsList()
		{
			List<SelectListItem> monthsList = new();
			// Loop through the months and create SelectListItem objects for each month
			for (int month = 1; month <= 12; month++)
			{
				string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
				SelectListItem listItem = new()
				{
					Text = monthName,
					Value = month.ToString() // Month number as the 'Value'
				};
				monthsList.Add(listItem);
			}
			return monthsList;
		}
		public async Task<IEnumerable<SelectListItem>> GetDropdownFromTableKeyValue(string tableKeyValue, string? filter)
		{
			var dropdownValues = await _mediaTr.Send(new GetDropdownValuesQuery(tableKeyValue, filter));
			List<SelectListItem> selectListItems = new();
			foreach (var item in dropdownValues)
			{
				string? key = item.ContainsKey("Key") ? item["Key"] : "";
				string? value = item.ContainsKey("Value") ? item["Value"] : "";
				selectListItems.Add(new SelectListItem
				{
					Text = value,
					Value = key
				});
			}
			return selectListItems;
		}
		public async Task<IList<Dictionary<string, string>>> GetReportList()
		{
			return await _mediaTr.Send(new GetReportListQuery());
		}
		public SelectList GetTaskListList(string? id)
		{
			return _appicationcontext.GetSingle<TaskListState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.TaskListCode } }, "Value", "Text", e.Id),
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
 
        public async Task<IEnumerable<SelectListItem>> GetDepartments(string? company)
        {
            var list = new List<SelectListItem>();
            var departmentList = await _appicationcontext.Department.Where(l=>l.CompanyCode == company)
				.OrderBy(a => a.CompanyCode).ToListAsync();
            if (departmentList != null)
            {
                foreach (var p in departmentList)
                    list.Add(new SelectListItem { Value = p.Id, Text = p.DepartmentName });
            }
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetEndorsers(string? companyid)
        {
            var list = new List<SelectListItem>();
			if(companyid != null)
			{
                var endorserList = await (from u in _identityContext.Users
                                          join ur in _identityContext.UserRoles on u.Id equals ur.UserId
                                          join r in _identityContext.Roles on ur.RoleId equals r.Id
                                          where u.CompanyId == companyid && r.Name == Core.Constants.Roles.Endorser
                                          select u).OrderBy(a => a.Name).ToListAsync();

                if (endorserList.Count() > 0)
                {
                    foreach (var e in endorserList)
                        list.Add(new SelectListItem { Value = e.Id, Text = e.Name });
                }
            }
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetApprovers(string? companyid)
        {
            var list = new List<SelectListItem>();
            if (companyid != null)
            {
				var approverList = await (from u in _identityContext.Users
									join ur in _identityContext.UserRoles on u.Id equals ur.UserId
									join r in _identityContext.Roles on ur.RoleId equals r.Id
									where u.CompanyId == companyid && r.Name == Core.Constants.Roles.Approver
									select u).OrderBy(a => a.Name).ToListAsync();

                if (approverList.Count() > 0)
                {
                    foreach (var a in approverList)
                        list.Add(new SelectListItem { Value = a.Id, Text = a.Name });
                }
            }
            return list;
        }
    }
}
