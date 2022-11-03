using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Activity.Queries;

public record GetActivityQuery : BaseQuery, IRequest<PagedListResponse<ActivityState>>;

public class GetActivityQueryHandler : BaseQueryHandler<ApplicationContext, ActivityState, GetActivityQuery>, IRequestHandler<GetActivityQuery, PagedListResponse<ActivityState>>
{
    public GetActivityQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ActivityState>> Handle(GetActivityQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ActivityState>().Include(l=>l.ClientFeedback).Include(l=>l.LeadTask).Include(l=>l.Lead).Include(l=>l.NextStep).Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
