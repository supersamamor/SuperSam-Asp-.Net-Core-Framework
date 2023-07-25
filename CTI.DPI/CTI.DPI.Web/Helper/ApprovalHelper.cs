using CTI.DPI.Application.Features.DPI.Approval.Queries;
using CTI.DPI.Core.DPI;
using MediatR;

namespace CTI.DPI.Web.Helper
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
                    return @"<span class=""badge bg-secondary"">" + approvalStatus + "</span>";
                case ApprovalStatus.ForApproval:
                    return @"<span class=""badge bg-info"">" + approvalStatus + "</span>";
                case ApprovalStatus.PartiallyApproved:
                    return @"<span class=""badge bg-primary"">" + approvalStatus + "</span>";
                case ApprovalStatus.Approved:
                    return @"<span class=""badge bg-success"">" + approvalStatus + "</span>";
                case ApprovalStatus.Rejected:
                    return @"<span class=""badge bg-danger"">" + approvalStatus + "</span>";
                default:
                    break;
            }
            return approvalStatus;
        }
    }
}
