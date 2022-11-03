using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.EntityGroup.Queries;

public record GetEntityGroupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<EntityGroupState>>;

public class GetEntityGroupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, EntityGroupState, GetEntityGroupByIdQuery>, IRequestHandler<GetEntityGroupByIdQuery, Option<EntityGroupState>>
{
    public GetEntityGroupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<EntityGroupState>> Handle(GetEntityGroupByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.EntityGroup.Include(l=>l.PPlusConnectionSetup)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
