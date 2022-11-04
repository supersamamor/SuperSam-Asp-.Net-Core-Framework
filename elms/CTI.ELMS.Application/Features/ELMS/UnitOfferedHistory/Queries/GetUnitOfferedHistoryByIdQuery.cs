using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Queries;

public record GetUnitOfferedHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitOfferedHistoryState>>;

public class GetUnitOfferedHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitOfferedHistoryState, GetUnitOfferedHistoryByIdQuery>, IRequestHandler<GetUnitOfferedHistoryByIdQuery, Option<UnitOfferedHistoryState>>
{
    public GetUnitOfferedHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitOfferedHistoryState>> Handle(GetUnitOfferedHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UnitOfferedHistory.Include(l=>l.OfferingHistory).Include(l=>l.Offering).Include(l=>l.Unit)
			.Include(l=>l.AnnualIncrementHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
