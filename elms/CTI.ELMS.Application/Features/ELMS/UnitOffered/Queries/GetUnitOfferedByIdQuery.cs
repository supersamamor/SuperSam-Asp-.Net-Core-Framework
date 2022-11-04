using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitOffered.Queries;

public record GetUnitOfferedByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitOfferedState>>;

public class GetUnitOfferedByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitOfferedState, GetUnitOfferedByIdQuery>, IRequestHandler<GetUnitOfferedByIdQuery, Option<UnitOfferedState>>
{
    public GetUnitOfferedByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitOfferedState>> Handle(GetUnitOfferedByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UnitOffered.Include(l=>l.Unit).Include(l=>l.Offering)
			.Include(l=>l.AnnualIncrementList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
