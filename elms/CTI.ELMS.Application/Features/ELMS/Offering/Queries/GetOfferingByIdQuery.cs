using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Queries;

public record GetOfferingByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OfferingState>>;

public class GetOfferingByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, OfferingState, GetOfferingByIdQuery>, IRequestHandler<GetOfferingByIdQuery, Option<OfferingState>>
{
    public GetOfferingByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Option<OfferingState>> Handle(GetOfferingByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.Offering.Include(l => l.Project).Include(l => l.Lead)
            .Include(l => l.OfferingHistoryList)
            .Include(l => l.PreSelectedUnitList!).ThenInclude(l => l.Unit)
            .Include(l => l.UnitOfferedList!).ThenInclude(l => l.Unit)
            .Include(l => l.UnitOfferedHistoryList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
