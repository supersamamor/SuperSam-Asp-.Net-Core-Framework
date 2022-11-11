using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Queries;

public record GetNextStepQuery : BaseQuery, IRequest<PagedListResponse<NextStepState>>
{
    public string? LeadTaskId { get; set; }
    public string? ClientFeedbackId { get; set; }
    
}

public class GetNextStepQueryHandler : BaseQueryHandler<ApplicationContext, NextStepState, GetNextStepQuery>, IRequestHandler<GetNextStepQuery, PagedListResponse<NextStepState>>
{
    public GetNextStepQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<NextStepState>> Handle(GetNextStepQuery request, CancellationToken cancellationToken = default)
	{
		var query = Context.NextStep.AsNoTracking();
		if (!string.IsNullOrEmpty(request.LeadTaskId) && !string.IsNullOrEmpty(request.ClientFeedbackId))
		{
			query = from a in query
					join b in Context.LeadTaskNextStep on a.Id equals b.NextStepId
					where b.LeadTaskId == request.LeadTaskId && b.ClientFeedbackId == request.ClientFeedbackId
					select a;
		}
		return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);
	}
}
