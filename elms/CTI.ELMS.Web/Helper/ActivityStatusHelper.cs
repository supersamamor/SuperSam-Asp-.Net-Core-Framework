using CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Queries;
using CTI.ELMS.Core.Constants;
using MediatR;

namespace CTI.ELMS.Web.Helper
{
    public class ActivityStatusHelper
    {
        private readonly IMediator _mediator;
        public ActivityStatusHelper(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string GetActivityStatus(string leadTaskId, string clientFeedbackId)
        {
            string? activityStatus = @"<span class=""badge bg-secondary"">N/A</span>";
            _ = _mediator.Send(new GetLeadTaskClientFeedBackByIdQuery("", leadTaskId, clientFeedbackId)).Result.Select(l => activityStatus = l.ActivityStatus);
            return activityStatus switch
            {
                ActivityStatus.Cold => @"<span class=""badge bg-info"">" + activityStatus + "</span>",
                ActivityStatus.Warm => @"<span class=""badge bg-warning"">" + activityStatus + "</span>",
                ActivityStatus.Hot => @"<span class=""badge bg-danger"">" + activityStatus + "</span>",
                ActivityStatus.TaggedAsAwarded => @"<span class=""badge bg-success"">" + activityStatus + "</span>",
                _ => @"<span class=""badge bg-secondary"">N/A</span>",
            };
        }
    }
}
