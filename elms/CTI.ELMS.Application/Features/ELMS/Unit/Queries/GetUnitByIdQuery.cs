using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Unit.Queries;

public record GetUnitByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitState>>;

public class GetUnitByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitState, GetUnitByIdQuery>, IRequestHandler<GetUnitByIdQuery, Option<UnitState>>
{
    public GetUnitByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitState>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Unit.Include(l=>l.Project)
			.Include(l=>l.UnitActivityList)
			.Include(l=>l.PreSelectedUnitList)
			.Include(l=>l.UnitOfferedList)
			.Include(l=>l.UnitOfferedHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
