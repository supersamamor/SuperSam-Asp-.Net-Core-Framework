using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ActivityHistory.Queries;

public record GetActivityHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ActivityHistoryState>>;

public class GetActivityHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ActivityHistoryState, GetActivityHistoryByIdQuery>, IRequestHandler<GetActivityHistoryByIdQuery, Option<ActivityHistoryState>>
{
    public GetActivityHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ActivityHistoryState>> Handle(GetActivityHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ActivityHistory.Include(l=>l.LeadTask).Include(l=>l.Activity).Include(l=>l.ClientFeedback).Include(l=>l.NextStep)
			.Include(l=>l.UnitActivityList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
