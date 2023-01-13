using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.City.Queries;

public record GetCityByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CityState>>;

public class GetCityByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CityState, GetCityByIdQuery>, IRequestHandler<GetCityByIdQuery, Option<CityState>>
{
    public GetCityByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CityState>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.City.Include(l=>l.Province)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
