using CTI.ContractManagement.Application.Features.ContractManagement.Approval.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using MediatR;

namespace CTI.ContractManagement.Web.Helper
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
