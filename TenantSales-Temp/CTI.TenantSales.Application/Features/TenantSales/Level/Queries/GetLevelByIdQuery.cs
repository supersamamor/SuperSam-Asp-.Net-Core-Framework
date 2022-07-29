using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Level.Queries;

public record GetLevelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LevelState>>;

public class GetLevelByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LevelState, GetLevelByIdQuery>, IRequestHandler<GetLevelByIdQuery, Option<LevelState>>
{
    public GetLevelByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<LevelState>> Handle(GetLevelByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Level.Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
