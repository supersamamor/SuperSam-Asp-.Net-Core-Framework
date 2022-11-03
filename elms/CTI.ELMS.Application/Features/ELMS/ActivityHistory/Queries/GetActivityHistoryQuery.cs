using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ActivityHistory.Queries;

public record GetActivityHistoryQuery : BaseQuery, IRequest<PagedListResponse<ActivityHistoryState>>;

public class GetActivityHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ActivityHistoryState, GetActivityHistoryQuery>, IRequestHandler<GetActivityHistoryQuery, PagedListResponse<ActivityHistoryState>>
{
    public GetActivityHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ActivityHistoryState>> Handle(GetActivityHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ActivityHistoryState>().Include(l=>l.LeadTask).Include(l=>l.Activity).Include(l=>l.ClientFeedback).Include(l=>l.NextStep)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
