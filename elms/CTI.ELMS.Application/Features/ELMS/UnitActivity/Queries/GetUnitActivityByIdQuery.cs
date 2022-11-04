using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitActivity.Queries;

public record GetUnitActivityByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitActivityState>>;

public class GetUnitActivityByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitActivityState, GetUnitActivityByIdQuery>, IRequestHandler<GetUnitActivityByIdQuery, Option<UnitActivityState>>
{
    public GetUnitActivityByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitActivityState>> Handle(GetUnitActivityByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UnitActivity.Include(l=>l.ActivityHistory).Include(l=>l.Unit).Include(l=>l.Activity)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
