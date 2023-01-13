using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Region.Queries;

public record GetRegionByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RegionState>>;

public class GetRegionByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, RegionState, GetRegionByIdQuery>, IRequestHandler<GetRegionByIdQuery, Option<RegionState>>
{
    public GetRegionByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<RegionState>> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Region.Include(l=>l.Country)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
