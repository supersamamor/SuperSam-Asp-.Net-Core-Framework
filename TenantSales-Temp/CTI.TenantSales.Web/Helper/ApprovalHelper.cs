using CTI.TenantSales.Application.Features.TenantSales.Approval.Queries;
using CTI.TenantSales.Core.TenantSales;
using MediatR;

namespace CTI.TenantSales.Web.Helper
{
    public class ApprovalHelper
    {
        private readonly IMediator _mediator;
        public ApprovalHelper(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string GetApprovalStatus(string dataId)
        {
            string approvalStatus = "";
            _ = _mediator.Send(new GetApprovalStatusByIdQuery(dataId)).Result.Select(l => approvalStatus = l);
            switch (approvalStatus)
            {
                case ApprovalStatus.New:
                    return @"<span class=""badge badge-secondary"">" + approvalStatus + "</span>";
                case ApprovalStatus.ForApproval:
                    return @"<span class=""badge badge-info"">" + approvalStatus + "</span>";
                case ApprovalStatus.PartiallyApproved:
                    return @"<span class=""badge badge-primary"">" + approvalStatus + "</span>";
                case ApprovalStatus.Approved:
                    return @"<span class=""badge badge-success"">" + approvalStatus + "</span>";
                case ApprovalStatus.Rejected:
                    return @"<span class=""badge badge-danger"">" + approvalStatus + "</span>";
                default:
                    break;
            }
            return approvalStatus;
        }
    }
}
