using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitGroup.Queries;

public record GetUnitGroupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitGroupState>>;

public class GetUnitGroupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitGroupState, GetUnitGroupByIdQuery>, IRequestHandler<GetUnitGroupByIdQuery, Option<UnitGroupState>>
{
    public GetUnitGroupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitGroupState>> Handle(GetUnitGroupByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UnitGroup.Include(l=>l.OfferingHistory)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
