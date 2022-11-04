using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Queries;

public record GetLeadTaskNextStepQuery : BaseQuery, IRequest<PagedListResponse<LeadTaskNextStepState>>;

public class GetLeadTaskNextStepQueryHandler : BaseQueryHandler<ApplicationContext, LeadTaskNextStepState, GetLeadTaskNextStepQuery>, IRequestHandler<GetLeadTaskNextStepQuery, PagedListResponse<LeadTaskNextStepState>>
{
    public GetLeadTaskNextStepQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<LeadTaskNextStepState>> Handle(GetLeadTaskNextStepQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<LeadTaskNextStepState>().Include(l=>l.LeadTask).Include(l=>l.NextStep).Include(l=>l.ClientFeedback)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
