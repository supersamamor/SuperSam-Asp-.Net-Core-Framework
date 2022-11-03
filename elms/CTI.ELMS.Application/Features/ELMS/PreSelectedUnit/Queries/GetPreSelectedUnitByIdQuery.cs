using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Queries;

public record GetPreSelectedUnitByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PreSelectedUnitState>>;

public class GetPreSelectedUnitByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PreSelectedUnitState, GetPreSelectedUnitByIdQuery>, IRequestHandler<GetPreSelectedUnitByIdQuery, Option<PreSelectedUnitState>>
{
    public GetPreSelectedUnitByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PreSelectedUnitState>> Handle(GetPreSelectedUnitByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PreSelectedUnit.Include(l=>l.Offering).Include(l=>l.Unit)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
