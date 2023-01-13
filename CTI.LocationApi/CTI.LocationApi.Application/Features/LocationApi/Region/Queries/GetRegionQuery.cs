using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Region.Queries;

public record GetRegionQuery : BaseQuery, IRequest<PagedListResponse<RegionState>>;

public class GetRegionQueryHandler : BaseQueryHandler<ApplicationContext, RegionState, GetRegionQuery>, IRequestHandler<GetRegionQuery, PagedListResponse<RegionState>>
{
    public GetRegionQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<RegionState>> Handle(GetRegionQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<RegionState>().Include(l=>l.Country)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
