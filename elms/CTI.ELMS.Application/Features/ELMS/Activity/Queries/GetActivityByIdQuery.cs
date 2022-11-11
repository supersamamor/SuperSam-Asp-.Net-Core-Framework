using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Activity.Queries;

public record GetActivityByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ActivityState>>;

public class GetActivityByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ActivityState, GetActivityByIdQuery>, IRequestHandler<GetActivityByIdQuery, Option<ActivityState>>
{
    public GetActivityByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Option<ActivityState>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.Activity.Include(l => l.LeadTask).Include(l => l.Lead).Include(l => l.Project).Include(l => l.ClientFeedback).Include(l => l.NextStep)
            .Include(l => l.ActivityHistoryList!).ThenInclude(l => l.LeadTask)
            .Include(l => l.ActivityHistoryList!).ThenInclude(l => l.ClientFeedback)
            .Include(l => l.ActivityHistoryList!).ThenInclude(l => l.NextStep)
            .Include(l => l.UnitActivityList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
