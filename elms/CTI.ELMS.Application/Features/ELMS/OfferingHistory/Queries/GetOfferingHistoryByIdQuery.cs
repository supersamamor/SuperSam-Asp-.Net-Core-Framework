using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.OfferingHistory.Queries;

public record GetOfferingHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OfferingHistoryState>>;

public class GetOfferingHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, OfferingHistoryState, GetOfferingHistoryByIdQuery>, IRequestHandler<GetOfferingHistoryByIdQuery, Option<OfferingHistoryState>>
{
    public GetOfferingHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Option<OfferingHistoryState>> Handle(GetOfferingHistoryByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.OfferingHistory.Include(l => l.Lead)
            .Include(l => l.UnitOfferedHistoryList)
            .Include(l => l.UnitGroupList)
            .Include(l => l.Offering)
            .Include(l => l.Project)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
