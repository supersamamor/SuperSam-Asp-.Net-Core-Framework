using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.ELMS.Infrastructure.Data;
using CTI.ELMS.Core.ELMS;
using CTI.Common.Data;
using CTI.ELMS.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.ELMS.Web.Areas.Admin.Queries.Roles;

namespace CTI.ELMS.Web.Service
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
            return _context.GetSingle<EntityGroupState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
        }
        public SelectList GetBusinessNatureList(string id)
        {
            return _context.GetSingle<BusinessNatureState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.BusinessNatureCode } }, "Value", "Text", e.Id),
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
            return _context.GetSingle<ProjectState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
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
        public SelectList GetBusinessNatureSubItemList(string id)
        {
            return _context.GetSingle<BusinessNatureSubItemState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
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
        public SelectList GetBusinessNatureCategoryList(string id)
        {
            return _context.GetSingle<BusinessNatureCategoryState>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
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
    }
}
