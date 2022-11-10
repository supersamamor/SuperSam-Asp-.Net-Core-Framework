using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.ELMS.Infrastructure.Data;
using CTI.ELMS.Core.ELMS;
using CTI.Common.Data;
using CTI.ELMS.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.ELMS.Web.Areas.Admin.Queries.Roles;
using Microsoft.EntityFrameworkCore;
using CTI.ELMS.LocationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Service
{
    public class DropdownServices
    {
        private readonly ApplicationContext _context;
        private readonly IMediator _mediaTr;
        private readonly LocationApiService _locationServiceApi;
        public DropdownServices(ApplicationContext context, IMediator mediaTr, LocationApiService locationApiService)
        {
            _context = context;
            _mediaTr = mediaTr;
            _locationServiceApi = locationApiService;
        }
        public SelectList GetOfferingList(string id)
        {
            return _context.GetSingle<OfferingState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetUnitList(string id)
        {
            return _context.GetSingle<UnitState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetEntityGroupList(string id)
        {
            var entityGroupList = _context.EntityGroup.Where(e => e.Id == id)
                .Include(l => l.PPlusConnectionSetup).ToList();
            if (entityGroupList.Count > 0)
            {
                return new SelectList(entityGroupList.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Value = a.Id,
                        Text = a.PPlusConnectionSetup!.PPlusVersionName
                        + " - " + a.PPLUSEntityCode
                        + " - " + a.EntityName
                    };
                }), "Value", "Text", id);
            }
            else
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
        }
        public SelectList GetBusinessNatureList(string id)
        {
            return _context.GetSingle<BusinessNatureState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.BusinessNatureName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetUnitOfferedList(string id)
        {
            return _context.GetSingle<UnitOfferedState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetProjectList(string id)
        {
            var projectList = _context.Project.Where(e => e.Id == id)
                .Include(l => l.EntityGroup).ThenInclude(l => l!.PPlusConnectionSetup).ToList();
            if (projectList.Count > 0)
            {
                return new SelectList(projectList.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Value = a.Id,
                        Text = a.EntityGroup!.PPlusConnectionSetup!.PPlusVersionName
                        + " - " + a.IFCAProjectCode
                        + " - " + a.ProjectName
                    };
                }), "Value", "Text", id);
            }
            else
            {
                return new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
        }
        public SelectList GetLeadList(string id)
        {
            return _context.GetSingle<LeadState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetIFCATenantInformationList(string id)
        {
            return _context.GetSingle<IFCATenantInformationState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetBusinessNatureSubItemList(string? id)
        {
            return _context.GetSingle<BusinessNatureSubItemState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.BusinessNatureSubItemName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetLeadTaskList(string id)
        {
            return _context.GetSingle<LeadTaskState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.LeadTaskName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetClientFeedbackList(string id)
        {
            return _context.GetSingle<ClientFeedbackState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.ClientFeedbackName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetNextStepList(string id)
        {
            return _context.GetSingle<NextStepState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.NextStepTaskName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetSalutationList(string id)
        {
            return _context.GetSingle<SalutationState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.SalutationDescription } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetOfferingHistoryList(string id)
        {
            return _context.GetSingle<OfferingHistoryState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetPPlusConnectionSetupList(string id)
        {
            return _context.GetSingle<PPlusConnectionSetupState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.PPlusVersionName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetUnitOfferedHistoryList(string id)
        {
            return _context.GetSingle<UnitOfferedHistoryState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetUnitBudgetList(string id)
        {
            return _context.GetSingle<UnitBudgetState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetActivityList(string id)
        {
            return _context.GetSingle<ActivityState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetActivityHistoryList(string id)
        {
            return _context.GetSingle<ActivityHistoryState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetBusinessNatureCategoryList(string? id)
        {
            return _context.GetSingle<BusinessNatureCategoryState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.BusinessNatureCategoryName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetLeadSourceList(string id)
        {
            return _context.GetSingle<LeadSourceState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.LeadSourceName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetOperationTypeList(string id)
        {
            return _context.GetSingle<OperationTypeState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.OperationTypeName } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetLeadTouchPointList(string id)
        {
            return _context.GetSingle<LeadTouchPointState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.LeadTouchPointName } }, "Value", "Text", e.Id),
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
        public IEnumerable<SelectListItem> ClientTypes()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.ClientType.SingleProprietorship, Value = Core.Constants.ClientType.SingleProprietorship, },
                new SelectListItem { Text = Core.Constants.ClientType.Corporation, Value = Core.Constants.ClientType.Corporation, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> ActivityStatusList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.ActivityStatus.Cold, Value = Core.Constants.ActivityStatus.Cold, },
                new SelectListItem { Text = Core.Constants.ActivityStatus.Warm, Value = Core.Constants.ActivityStatus.Warm, },
                new SelectListItem { Text = Core.Constants.ActivityStatus.Hot, Value = Core.Constants.ActivityStatus.Hot, },
                new SelectListItem { Text = Core.Constants.ActivityStatus.TaggedAsAwarded, Value = Core.Constants.ActivityStatus.TaggedAsAwarded, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> ANTypeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.ANType.Fixed, Value = Core.Constants.ANType.Fixed, },
                new SelectListItem { Text = Core.Constants.ANType.NonFixed, Value = Core.Constants.ANType.NonFixed, },
                new SelectListItem { Text = Core.Constants.ANType.FixedKiosk, Value = Core.Constants.ANType.FixedKiosk, },
                new SelectListItem { Text = Core.Constants.ANType.NonFixedKiosk, Value = Core.Constants.ANType.NonFixedKiosk, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> ANTermTypeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.ANTermType.Inline, Value = Core.Constants.ANTermType.Inline, },
                new SelectListItem { Text = Core.Constants.ANTermType.Kiosk, Value = Core.Constants.ANTermType.Kiosk, },
            };
            return items;
        }
        public IEnumerable<SelectListItem> ContactTypeList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = Core.Constants.ContactType.Email, Value = Core.Constants.ContactType.Email, },
                new SelectListItem { Text = Core.Constants.ContactType.ContactNumber, Value = Core.Constants.ContactType.ContactNumber, },
            };
            return items;
        }
        public async Task<IEnumerable<SelectListItem>> GetCountryList()
        {
            var list = new List<SelectListItem>();
            var countryList = await _locationServiceApi.GetCountryList();
            if (countryList != null)
            {
                foreach (var c in countryList.OrderBy(a => a.Name))
                    if (c.Name.Equals(WebConstants.CountryPhilippines))
                    {
                        list.Insert(0, new SelectListItem { Value = c.Name, Text = c.Name, Group = new SelectListGroup { Name = c.Name } });
                    }
                    else
                    {
                        list.Add(new SelectListItem { Value = c.Name, Text = c.Name, Group = new SelectListGroup { Name = c.Name } });
                    }
            }
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetProvinceList()
        {
            var list = new List<SelectListItem>();
            var provinceList = await _locationServiceApi.GetProvinceList();
            if (provinceList != null)
            {
                foreach (var p in provinceList)
                    list.Add(new SelectListItem { Value = p.AlternativeName, Text = p.AlternativeName });
            }
            return list;
        }

        public async Task<IActionResult> GetCityList(string province)
        {
            var list = new List<SelectListItem>();
            var cityList = await _locationServiceApi.GetCityListByProvinceName(province);
            if (cityList != null)
            {
                foreach (var p in cityList)
                    list.Add(new SelectListItem { Value = p.AlternativeName, Text = p.AlternativeName });
            }          
            return new JsonResult(list);
        }

        public async Task<IActionResult> GetBarangayList(string province, string city)
        {
            var list = new List<SelectListItem>();
            var brgyList = await _locationServiceApi.GetBarangayListByProvinceNameAndCityName(province, city);
            if (brgyList != null)
            {
                foreach (var p in brgyList)
                    list.Add(new SelectListItem { Value = p.AlternativeName, Text = p.AlternativeName });
            }            
            return new JsonResult(list);
        }
    }
}
