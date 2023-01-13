using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.City.Queries;

public record GetCityQuery : BaseQuery, IRequest<PagedListResponse<CityState>>;

public class GetCityQueryHandler : BaseQueryHandler<ApplicationContext, CityState, GetCityQuery>, IRequestHandler<GetCityQuery, PagedListResponse<CityState>>
{
    public GetCityQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CityState>> Handle(GetCityQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CityState>().Include(l=>l.Province)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
